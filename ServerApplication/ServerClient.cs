using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RHApplicationLib.Abstract;
using RHApplicationLib.Core;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ServerApplication
{
    class ServerClient
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private bool isConnected;
        private bool isClient;

        private bool isRunning = false;

        private DateTime startTimeSession;

        public string UserName { get; set; }
        public string SessionId { get; set; }
        public object BikeDataLock { get; } = new object();

        private int receivedBytes;
        private byte[] receiveBuffer;

        public ServerClient(TcpClient tcpClient)
        {
            isConnected = true;
            this.tcpClient = tcpClient;

            this.stream = this.tcpClient.GetStream();

            this.receivedBytes = 0;
            this.receiveBuffer = new byte[4];

            stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        private void ReceiveLengthInt(IAsyncResult ar)
        {
            try
            {
                this.stream.EndRead(ar);

                int dataLength = BitConverter.ToInt32(Utility.ReverseIfBigEndian(this.receiveBuffer));

                // create data buffer
                this.receivedBytes = 0;
                this.receiveBuffer = new byte[dataLength];

                this.stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, new AsyncCallback(ReceiveData), null);
            }
            catch (Exception ex)
            {
                // Stream closed/error
                Console.WriteLine(ex.Message);
            }
        }

        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                this.receivedBytes += this.stream.EndRead(ar);

                if (this.receivedBytes < this.receiveBuffer.Length)
                {
                    this.stream.BeginRead(this.receiveBuffer, this.receivedBytes, this.receiveBuffer.Length - this.receivedBytes, this.ReceiveData, null);
                    return;
                }

                string data = Encoding.ASCII.GetString(this.receiveBuffer);

                DataPacket dataPacket = JsonConvert.DeserializeObject<DataPacket>(data);
                handleData(dataPacket);

                this.receivedBytes = 0;
                this.receiveBuffer = new byte[4];
                this.stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, new AsyncCallback(ReceiveLengthInt), null);
            }
            catch (Exception ex)
            {
                // Stream closed/error
                Console.WriteLine(ex.Message);
            }
        }

        private void handleData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGIN":
                    {
                        Server.clientList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ClientList.txt"));
                        Server.doctorList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("DoctorList.txt"));

                        Console.WriteLine("Received a login packet");
                        DataPacket<LoginPacket> d = data.GetData<LoginPacket>();

                        if (Server.clientList.ContainsKey(d.data.username) && d.data.isClient == true)
                        {
                            if (Server.clientList[d.data.username] == d.data.password)
                            {
                                this.isClient = true;
                                this.UserName = d.data.username;
                                Server.tempList.RemoveClient(this);
                                Server.clients.AddClient(this);
                                SendData(new DataPacket<LoginResponse>()
                                {
                                    sender = this.UserName,
                                    type = "LOGINRESPONSE",
                                    data = new LoginResponse()
                                    {
                                        isClient = true,
                                        status = "OK"
                                    }
                                }.ToJson()); ;
                            }
                            else
                            {
                                SendData(new DataPacket<LoginResponse>()
                                {
                                    sender = this.UserName,
                                    type = "LOGINRESPONSE",
                                    data = new LoginResponse()
                                    {
                                        isClient = true,
                                        status = "ERROR"
                                    }
                                }.ToJson());
                            }
                        }
                        else if (Server.doctorList.ContainsKey(d.data.username) && d.data.isClient == false)
                        {
                            if (Server.doctorList[d.data.username] == d.data.password)
                            {
                                this.isClient = false;
                                this.UserName = d.data.username;
                                Server.tempList.RemoveClient(this);
                                Server.doctors.AddClient(this);
                                SendData(new DataPacket<LoginResponse>()
                                {
                                    sender = this.UserName,
                                    type = "LOGINRESPONSE",
                                    data = new LoginResponse()
                                    {
                                        isClient = false,
                                        status = "OK"
                                    }
                                }.ToJson());
                            }
                            else
                            {
                                SendData(new DataPacket<LoginResponse>()
                                {
                                    sender = this.UserName,
                                    type = "LOGINRESPONSE",
                                    data = new LoginResponse()
                                    {
                                        isClient = false,
                                        status = "ERROR"
                                    }
                                }.ToJson());
                            }
                        }
                        else
                        {
                            SendData(new DataPacket<LoginResponse>()
                            {
                                sender = this.UserName,
                                type = "LOGINRESPONSE",
                                data = new LoginResponse()
                                {
                                    isClient = false,
                                    status = "ERROR"
                                }
                            }.ToJson());
                        }
                        break;
                    }
                case "CHAT":
                    {
                        Console.WriteLine("Received a chat packet");

                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();
                        if (d.data.receiver == "All") 
                        {
                            foreach (ServerClient client in Server.clients.GetClients())
                            {
                                SendDataToUser(client, d.ToJson());
                            }
                        }
                        else
                        {
                            if (Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver) != null)
                            {
                                SendDataToUser(Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver), d.ToJson());
                            }
                            else if (Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver) != null)
                            {
                                SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver), d.ToJson());
                            }
                        }
                        break;
                    }
                case "BIKEDATA":
                    {
                        DataPacket<BikeDataPacket> d = data.GetData<BikeDataPacket>();

                        if(!string.IsNullOrEmpty(this.SessionId))
                        {
                            lock(this.BikeDataLock)
                            {
                                try
                                {
                                    using (StreamWriter fileStream = new StreamWriter(new FileStream("Trainingen\\" + this.UserName + "\\" + this.SessionId + ".json", FileMode.Append, FileAccess.Write)))
                                    {
                                        fileStream.Write(d.data.ToJson());
                                        fileStream.Write(',');
                                        fileStream.Flush();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
                                }
                            }
                        }
                    
                        if (d.data.doctor != null)
                        {
                            if (Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.doctor) != null)
                            {
                                SendDataToUser(Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.doctor), d.ToJson());
                            }
                        }
                        break;
                    }
                case "REQUEST_TRAINING_LIST":
                    {
                        DataPacket<RequestTrainingList> d = data.GetData<RequestTrainingList>();

                        ResponseTrainingList result = new ResponseTrainingList();

                        string trainingDirPath = $"Trainingen\\{d.data.forClient}";
                        if (!string.IsNullOrEmpty(d.data.forClient) && Directory.Exists(trainingDirPath))
                        {
                            result.forClient = d.data.forClient;
                            string[] trainingFiles = Directory.GetFiles(trainingDirPath);

                            result.trainingList = trainingFiles.Where(f => Path.GetExtension(f) == ".json")
                                .Select(f => Path.GetFileNameWithoutExtension(f))
                                .ToList();
                        }

                        this.SendData(new DataPacket<ResponseTrainingList>
                        {
                            sender = this.UserName,
                            type = "RESPONSE_TRAINING_LIST",
                            data = result
                        }.ToJson());
                    }
                    break;
                case "REQUEST_TRAINING_DATA":
                    {
                        DataPacket<RequestTrainingData> d = data.GetData<RequestTrainingData>();

                        ResponseTrainingData result = new ResponseTrainingData();

                        string trainingFilePath = $"Trainingen\\{d.data.forClient}\\{d.data.trainingName}.json";
                        if(!string.IsNullOrEmpty(d.data.forClient) && !string.IsNullOrEmpty(d.data.trainingName) && File.Exists(trainingFilePath))
                        {
                            result.forClient = d.data.forClient;
                            result.trainingName = d.data.trainingName;

                            try
                            {
                                using (StreamReader reader = File.OpenText(trainingFilePath))
                                {
                                    result.trainingData = JsonConvert.DeserializeObject<List<BikeDataPacket>>(reader.ReadToEnd());
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                                result.trainingData = new List<BikeDataPacket>();
                            }
                        } 

                        this.SendData(new DataPacket<ResponseTrainingData>
                        {
                            sender = this.UserName,
                            type = "RESPONSE_TRAINING_DATA",
                            data = result
                        }.ToJson());
                    }
                    break;
                case "REQUEST_CLIENTLIST":
                    {
                        Dictionary<string, bool> temp = new Dictionary<string, bool>();
                        foreach (string userName in Server.clientList.Keys)
                        {
                            temp.Add(userName, Server.clients.GetClients().FirstOrDefault(client => client.UserName == userName) != null);
                        }

                        SendData(new DataPacket<ClientListPacket>()
                        {
                            sender = this.UserName,
                            type = "RESPONSE_CLIENTLIST",
                            data = new ClientListPacket()
                            {
                                clientList = temp
                            }

                        }.ToJson());
                        break;
                    }
                case "USERNAME":
                    {
                        DataPacket<UserNamePacket> d = data.GetData<UserNamePacket>();

                        if (Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.clientUserName) != null)
                        {
                            SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.clientUserName), new DataPacket<UserNamePacketResponse>()
                            {
                                sender = this.UserName,
                                type = "USERNAME_RESPONSE",
                                data = new UserNamePacketResponse()
                                {
                                    doctorUserName = d.sender
                                }
                            }.ToJson());
                        }
                        break;
                    }
                case "DISCONNECT_LIVESESSION":
                    {
                        DataPacket<UserNamePacket> d = data.GetData<UserNamePacket>();

                        if (Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.clientUserName) != null)
                        {
                            SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.clientUserName), d.ToJson());
                        }
                        break;
                    }
                case "RESISTANCE":
                    {
                        DataPacket<ResistancePacket> d = data.GetData<ResistancePacket>();
                       
                        if (Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver) != null)
                        {
                            SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver), d.ToJson());
                        }
                        break;
                    }
                case "START_SESSION":
                    {
                        this.startTimeSession = DateTime.Now;
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();

                        ServerClient receiver = Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver);

                        if (receiver != null)
                        {
                            receiver.isRunning = true;
                            SendDataToUser(receiver, d.ToJson());

                            if (Directory.Exists("Trainingen\\" + receiver.UserName))
                            {
                                List<string> trainingFiles = Directory.GetFiles("Trainingen\\" + receiver.UserName)
                                    .Select((path) => Path.GetFileNameWithoutExtension(path))
                                    .ToList();

                                trainingFiles.Sort();
                                
                                string lastTrainingFileName = trainingFiles.LastOrDefault();

                                if(!string.IsNullOrEmpty(lastTrainingFileName))
                                {
                                    string lastTrainingId = lastTrainingFileName.Split(' ').LastOrDefault();

                                    if (int.TryParse(lastTrainingId, out int trainingsId)) receiver.SessionId = "Training " + (++trainingsId);
                                }
                            }
                            
                            if(string.IsNullOrEmpty(receiver.SessionId))
                            {
                                try
                                {
                                    Directory.CreateDirectory("Trainingen\\" + receiver.UserName);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
                                }
                                
                                receiver.SessionId = "Training 1";
                            }

                            lock(receiver.BikeDataLock)
                            {
                                try
                                {
                                    using (StreamWriter fileStream = new StreamWriter(new FileStream("Trainingen\\" + receiver.UserName + "\\" + receiver.SessionId + ".json", FileMode.Create, FileAccess.Write)))
                                    {
                                        fileStream.Write('[');
                                        fileStream.Flush();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
                                }
                            }

                            SendDataToUser(this, new DataPacket<ResponseSessionStatePacket>()
                            {
                                sender = this.UserName,
                                type = "RESPONSE_SESSIONSTATE",
                                data = new ResponseSessionStatePacket()
                                {
                                    receiver = d.data.receiver,
                                    sessionState = receiver.isRunning,
                                    startTimeSession = this.startTimeSession,
                                    sessionId = receiver.SessionId
                                }
                            }.ToJson());
                        }
                        break;
                    }
                case "STOP_SESSION":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();

                        ServerClient receiver = Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver);

                        if (receiver != null)
                        {
                            receiver.isRunning = false;
                            SendDataToUser(receiver, d.ToJson());

                            if (!string.IsNullOrEmpty(receiver.SessionId))
                            {
                                lock (receiver.BikeDataLock)
                                {
                                    try
                                    {
                                        using (StreamWriter fileStream = new StreamWriter(new FileStream("Trainingen\\" + receiver.UserName + "\\" + receiver.SessionId + ".json", FileMode.Append, FileAccess.Write)))
                                        {
                                            fileStream.Write(']');
                                            fileStream.Flush();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
                                    }
                                }
                            }

                            SendDataToUser(this, new DataPacket<ResponseSessionStatePacket>()
                            {
                                sender = this.UserName,
                                type = "RESPONSE_SESSIONSTATE",
                                data = new ResponseSessionStatePacket()
                                {
                                    receiver = d.data.receiver,
                                    sessionState = Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver).isRunning,
                                    startTimeSession = this.startTimeSession,
                                    sessionId = receiver.SessionId
                                }
                            }.ToJson());

                            receiver.SessionId = null;
                        }
                        break;
                    }
                case "REQUEST_SESSIONSTATE":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();

                        ServerClient receiver = Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver);

                        if (receiver != null)
                        {
                            SendDataToUser(this, new DataPacket<ResponseSessionStatePacket>()
                            {
                                sender = this.UserName,
                                type = "RESPONSE_SESSIONSTATE",
                                data = new ResponseSessionStatePacket()
                                {
                                    receiver = d.data.receiver,
                                    sessionState = receiver.isRunning,
                                    startTimeSession = this.startTimeSession,
                                    sessionId = receiver.SessionId
                                }
                            }.ToJson()) ;
                        }
                        break;
                    }
                case "SESSIONSTATE_RESPONSE":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();
                        if (Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver) != null)
                        {
                            SendDataToUser(Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver), d.ToJson());
                        }
                            break;
                    }
                case "INVALID_BIKE":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();
                        if (Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver) != null)
                        {
                            SendDataToUser(Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver), d.ToJson());
                        }

                        if (!string.IsNullOrEmpty(this.SessionId))
                        {
                            lock (this.BikeDataLock)
                            {
                                try
                                {
                                    using (StreamWriter fileStream = new StreamWriter(new FileStream("Trainingen\\" + this.UserName + "\\" + this.SessionId + ".json", FileMode.Append, FileAccess.Write)))
                                    {
                                        fileStream.Write(']');
                                        fileStream.Flush();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
                                }
                            }

                            this.SessionId = null;
                        }

                        break;
                    }
                case "SERVER_MESSAGE":
                    {
                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();
                        if (Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver) != null)
                        {
                            SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver), d.ToJson());
                        }
                        break;
                    }
                default:
                    Console.WriteLine("Unkown packetType");
                    break;
            }
        }

        private void SendData(string message)
        {
            if (this.isConnected)
            {
                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(message));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void SendDataToUser(ServerClient user, string data)
        {
            if (this.isConnected)
            {
                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(data));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                user.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void SendData(DataPacket data)
        {
            //TODO Deze methode moet verder afgemaakt worden, en gecheckt worden
            if (this.isConnected)
            {
                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        //public void SendChatToDoctor(DataPacket packet)
        //{

        //}
    }
}

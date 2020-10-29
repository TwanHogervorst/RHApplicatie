using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using RHApplicatieLib.Data;
using RHApplicatieLib.Core;

namespace DoctorApplication.Core
{

    public delegate void LoginCallback(bool status);
    public delegate void ChatCallback(string sender, string receiver, string message, bool isDoctorMessage);
    public delegate void ClientListCallback(Dictionary<string, bool> clientList);
    public delegate void BikeDataCallback(DataPacket<BikeDataPacket> DataPacket);
    public delegate void SessionStateCallback(string clientUserName, DateTime startTimeSession, bool state);
    public delegate void SessionStateMessageCallback(string sender, bool state);
    public delegate void BikeStateCallback(string sender, bool bikeIsConnected);
    public delegate void EmergencyResponseCallback(string sender, bool state);
    public delegate void TrainingListCallback(string forClient, List<string> trainingList);
    public delegate void TrainingDataCallback(string forClient, string trainingName, List<BikeDataPacket> trainingData);

    public class DoctorClient
    {
        private TcpClient client;
        private NetworkStream stream;
        public string username;
        private bool loggedIn = false;
        private string clientUserName;
        public string selectedUser;
        private bool isDisconnecting = false;

        public event LoginCallback OnLogin;
        public event ChatCallback OnChatReceived;
        public event ClientListCallback OnClientListReceived;
        public event BikeStateCallback OnBikeStateChanged;
        public event EmergencyResponseCallback OnEmergencyResponse;
        public event BikeDataCallback OnBikeDataReceived;
        public event SessionStateCallback OnSessionStateReceived;
        public event SessionStateMessageCallback OnSessionStateMessageReceived;
        public event TrainingListCallback OnTrainingListReceived;
        public event TrainingDataCallback OnTrainingDataReceived;

        private int receivedBytes;
        private byte[] receiveBuffer;

        public void RequestSessionState()
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<RequestSessionStatePacket>()
                {
                    sender = this.username,
                    type = "REQUEST_SESSIONSTATE",
                    data = new RequestSessionStatePacket()
                    {
                        receiver = clientUserName
                    }
                }.ToJson());
            }
        }

        public DoctorClient()
        {
            this.client = new TcpClient();
            this.client.BeginConnect("localhost", 15243, new AsyncCallback(Connect), null);
        }

        public void Connect(IAsyncResult ar)
        {
            this.client.EndConnect(ar);

            if (!this.loggedIn)
            {
                this.stream = client.GetStream();
            }

            this.receiveBuffer = new byte[4];
            this.receivedBytes = 0;
            this.stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        public void SendLogin(string username, string password)
        {
            this.username = username;
            //Send username and password to check
            SendData(new DataPacket<LoginPacket>()
            {
                type = "LOGIN",
                data = new LoginPacket()
                {
                    isClient = false,
                    username = username,
                    password = password

                }
            }.ToJson());
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
                Disconnect();
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

                try
                {
                    handleData(dataPacket);
                }
                catch
                {
                    // jammer dan
                }

                this.receivedBytes = 0;
                this.receiveBuffer = new byte[4];
                this.stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, new AsyncCallback(ReceiveLengthInt), null);
            }
            catch (Exception ex)
            {
                Disconnect();
                Console.WriteLine(ex.Message);
            }
        }



        private void SendData(string data)
        {
            if(this.client.Connected && this.stream.CanWrite)
            {
                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(data));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void SendChatMessage(string message)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<ChatPacket>()
                {
                    sender = this.username,
                    type = "CHAT",
                    data = new ChatPacket()
                    {
                        receiver = clientUserName,
                        chatMessage = message,
                        isDoctorMessage = true
                    }
                }.ToJson());
            }
        }

        public void SendServerMessage(string receiver, string message)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<ChatPacket>()
                {
                    sender = this.username,
                    type = "SERVER_MESSAGE",
                    data = new ChatPacket()
                    {
                        receiver = clientUserName,
                        chatMessage = message,
                        isDoctorMessage = true
                    }
                }.ToJson());
            }
        }

        public void BroadCast(string message)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<ChatPacket>()
                {
                    sender = this.username,
                    type = "CHAT",
                    data = new ChatPacket()
                    {
                        receiver = "All",
                        chatMessage = message,
                        isDoctorMessage = true
                    }
                }.ToJson());
            }
        }

        public void SendResistance(int resistance)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<ResistancePacket>()
                {
                    sender = this.username,
                    type = "RESISTANCE",
                    data = new ResistancePacket()
                    {
                        receiver = this.clientUserName,
                        resistance = resistance
                    }
                }.ToJson());
            }
        }

        public void StartSession()
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "START_SESSION",
                    data = new StartStopPacket()
                    {
                        receiver = this.clientUserName,
                        doctor = this.username,
                        startSession = true
                    }
                }.ToJson());
            }
        }

        public void StopSession()
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "STOP_SESSION",
                    data = new StartStopPacket()
                    {
                        receiver = clientUserName,
                        startSession = false
                    }
                }.ToJson());
            }
        }

        public void EmergencyStopSession(string _selectedUser)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<EmergencyStopPacket>()
                {
                    sender = this.username,
                    type = "EMERGENCY_STOP",
                    data = new EmergencyStopPacket()
                    {
                        receiver = _selectedUser,
                        startSession = false,
                        resistance = 0
                    }
                }.ToJson());
            }
        }

        public void RequestClientList()
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket()
                {
                    sender = this.username,
                    type = "REQUEST_CLIENTLIST"
                }.ToJson());
            }
        }

        public void SendUserName(string userNameClient)
        {
            if (this.loggedIn)
            {
                this.clientUserName = userNameClient;
                this.SendData(new DataPacket<UserNamePacket>()
                {
                    sender = this.username,
                    type = "USERNAME",
                    data = new UserNamePacket()
                    {
                        clientUserName = userNameClient
                    }
                }.ToJson());
            }
        }

        public void OnCloseLiveSession()
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<UserNamePacket>()
                {
                    sender = this.username,
                    type = "DISCONNECT_LIVESESSION",
                    data = new UserNamePacket()
                    {
                        clientUserName = this.clientUserName
                    }
                }.ToJson());

                this.clientUserName = null;
            }
        }

        public void RequestTrainingList(string forClient)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<RequestTrainingList>()
                {
                    sender = this.username,
                    type = "REQUEST_TRAINING_LIST",
                    data = new RequestTrainingList
                    {
                        forClient = forClient
                    }
                }.ToJson());
            }
        }

        public void RequestTrainingData(string forClient, string trainingName)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<RequestTrainingData>()
                {
                    sender = this.username,
                    type = "REQUEST_TRAINING_DATA",
                    data = new RequestTrainingData
                    {
                        forClient = forClient,
                        trainingName = trainingName
                    }
                }.ToJson());
            }
        }

        public void Disconnect()
        {
            if (!this.isDisconnecting)
            {
                this.isDisconnecting = true;

                MessageBox.Show("You are disconnected, this application is closing.", "Disconnect");
                this.stream.Close();
                this.client.Close();
                Application.Exit();

            }
        }

        public void DisconnectDoctor()
        {
            try
            {
                if (this.loggedIn)
                {
                    this.SendData(new DataPacket<DisconnectRequestPacket>()
                    {
                        sender = this.username,
                        type = "DISCONNECT",
                        data = new DisconnectRequestPacket()
                        {
                        }
                    }.ToJson());

                    this.stream.Flush();

                    Disconnect();
                }
            }
            catch
            {
                Disconnect();
            }
        }

        public void RequestBikeState(string forClient)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<RequestBikeStatePacket>
                {
                    type = "REQUEST_BIKE_STATE",
                    sender = this.username,
                    data = new RequestBikeStatePacket
                    {
                        forClient = forClient
                    }
                }.ToJson());
            }
        }

        private void handleData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGINRESPONSE":
                    {
                        DataPacket<LoginResponse> d = data.GetData<LoginResponse>();
                        if (d.data.status == "OK" && !d.data.isClient)
                        {
                            this.loggedIn = true;
                            OnLogin?.Invoke(this.loggedIn);
                            Console.WriteLine("You are logged in!");
                        }
                        else if (d.data.status == ("ERROR"))
                        {
                            this.loggedIn = false;
                            OnLogin?.Invoke(this.loggedIn);
                            Console.WriteLine("Your username and/or password is not valid!");

                        }
                        break;
                    }
                case "CHAT":
                    {
                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();

                        OnChatReceived?.Invoke(d.sender, d.data.receiver, $"{d.sender}: {d.data.chatMessage}\r\n", d.data.isDoctorMessage);
                        break;
                    }
                case "RESPONSE_CLIENTLIST":
                    {
                        DataPacket<ClientListPacket> d = data.GetData<ClientListPacket>();
                        OnClientListReceived?.Invoke(d.data.clientList);
                        break;
                    }
                case "BIKEDATA":
                    {
                        DataPacket<BikeDataPacket> d = data.GetData<BikeDataPacket>();
                        OnBikeDataReceived?.Invoke(d);
                        break;
                    }
                case "RESPONSE_SESSIONSTATE":
                    {
                        DataPacket<ResponseSessionStatePacket> d = data.GetData<ResponseSessionStatePacket>();
                        OnSessionStateReceived?.Invoke(d.data.receiver, d.data.startTimeSession, d.data.sessionState);
                        break;
                    }
                case "SESSIONSTATE_RESPONSE":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();
                        OnSessionStateMessageReceived?.Invoke(d.sender, d.data.startSession);
                        break;
                    }
                case "RESPONSE_BIKE_STATE":
                    {
                        DataPacket<ResponseBikeState> d = data.GetData<ResponseBikeState>();
                        OnBikeStateChanged?.Invoke(d.sender, d.data.bikeIsConnected);
                        break;
                    }
                case "SESSIONSTATE_EMERGENCYRESPONSE":
                    {
                        DataPacket<EmergencyResponsePacket> d = data.GetData<EmergencyResponsePacket>();
                        OnEmergencyResponse?.Invoke(d.sender, d.data.state);
                        break;
                    }
                case "RESPONSE_TRAINING_LIST":
                    {
                        DataPacket<ResponseTrainingList> d = data.GetData<ResponseTrainingList>();
                        this.OnTrainingListReceived?.Invoke(d.data.forClient, d.data.trainingList);
                        break;
                    }
                case "RESPONSE_TRAINING_DATA":
                    {
                        DataPacket<ResponseTrainingData> d = data.GetData<ResponseTrainingData>();
                        this.OnTrainingDataReceived?.Invoke(d.data.forClient, d.data.trainingName, d.data.trainingData);
                        break;
                    }
                case "DISCONNECT_REQUEST":
                    {
                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();
                        Disconnect();
                        break;
                    }
                default:
                    Console.WriteLine("Type is not valid");
                    break;
            }

        }
    }
}

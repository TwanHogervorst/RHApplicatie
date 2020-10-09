using Newtonsoft.Json;
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
        private byte[] buffer = new byte[4];
        private bool isConnected;
        private bool isClient;

        public string UserName { get; set; }

        public ServerClient(TcpClient tcpClient)
        {
            isConnected = true;
            this.tcpClient = tcpClient;

            this.stream = this.tcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        private void ReceiveLengthInt(IAsyncResult ar)
        {
            int dataLength = BitConverter.ToInt32(Utility.ReverseIfBigEndian(this.buffer));

            // create data buffer
            this.buffer = new byte[dataLength];

            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(ReceiveData), null);
        }

        private void ReceiveData(IAsyncResult ar)
        {
            string data = System.Text.Encoding.ASCII.GetString(this.buffer);

            DataPacket dataPacket = JsonConvert.DeserializeObject<DataPacket>(data);
            handleData(dataPacket);

            this.buffer = new byte[4];
            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
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
                        if (Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver) != null)
                        {
                          SendDataToUser(Server.doctors.GetClients().FirstOrDefault(doctor => doctor.UserName == d.data.receiver), d.ToJson());
                        }
                        else if (Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver) != null)
                        {
                            SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.receiver), d.ToJson());
                        }
                        break;
                    }
                case "BIKEDATA":
                    {
                        string pathToClientData = "ClientData.txt";
                        Dictionary<string, List<BikeDataPacket>> clientData_ =
                            JsonConvert.DeserializeObject<Dictionary<string, List<BikeDataPacket>>>
                            (
                            File.ReadAllText(pathToClientData)
                            );

                        Console.WriteLine("Received a bikeData packet");
                        DataPacket<BikeDataPacket> d = data.GetData<BikeDataPacket>();

                        if (clientData_.ContainsKey(d.sender))
                        {
                            clientData_[d.sender].Add(d.data);

                            string contentClientData = JsonConvert.SerializeObject(clientData_);
                            File.WriteAllText(pathToClientData, contentClientData);
                        }
                        else
                        {
                            clientData_.Add(d.sender, new List<BikeDataPacket>() { d.data });
                            string contentClientData = JsonConvert.SerializeObject(clientData_);
                            File.WriteAllText(pathToClientData, contentClientData);
                        }
                        break;
                    }
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

                        }.ToJson()
                        );
                        break;
                    }
                case "USERNAME":
                    {
                        DataPacket<UserNamePacket> d = data.GetData<UserNamePacket>();

                        SendDataToUser(Server.clients.GetClients().FirstOrDefault(client => client.UserName == d.data.clientUserName), new DataPacket<UserNamePacketResponse>()
                        {
                            sender = this.UserName,
                            type = "USERNAME_RESPONSE",
                            data = new UserNamePacketResponse()
                            {
                                doctorUserName = d.sender
                            }
                        }.ToJson());
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

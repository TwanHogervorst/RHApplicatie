using Newtonsoft.Json;
using RHApplicationLib.Core;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace DoctorApplication
{

    public delegate void LoginCallback(bool status);
    public delegate void ChatCallback(string sender, string message);
    public delegate void ClientListCallback(Dictionary<string, bool> clientList);
    public delegate void BikeDataCallback(DataPacket<BikeDataPacket> DataPacket);
    public delegate void SessionStateCallback(string clientUserName, DateTime startTimeSession, bool state);
    public delegate void SessionStateMessageCallback(string sender, bool state);
    public delegate void InvalidBikeCallback(string sender);

    public class DoctorClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[4];
        public string username;
        private bool loggedIn = false;
        private string clientUserName;

        public event LoginCallback OnLogin;
        public event ChatCallback OnChatReceived;
        public event ClientListCallback OnClientListReceived;
        public event InvalidBikeCallback OnInvalidBikeReceived;

        public void RequestSessionState()
        {
            if (this.loggedIn)
            {
                DataPacket<RequestSessionStatePacket> dataPacket = new DataPacket<RequestSessionStatePacket>()
                {
                    sender = this.username,
                    type = "REQUEST_SESSIONSTATE",
                    data = new RequestSessionStatePacket()
                    {
                        receiver = clientUserName
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public event BikeDataCallback OnBikeDataReceived;
        public event SessionStateCallback OnSessionStateReceived;
        public event SessionStateMessageCallback OnSessionStateMessageReceived;

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
            this.stream.BeginRead(this.buffer, 0, this.buffer.Length, new AsyncCallback(ReceiveLengthInt), null);
        }

        public void SendLogin(string username, string password)
        {
            this.username = username;
            //Send username and password to check
            sendData(new DataPacket<LoginPacket>()
            {
                type = "LOGIN",
                data = new LoginPacket()
                {
                    isClient = false,
                    username = username,
                    password = password

                }
            });
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



        private void sendData(DataPacket<LoginPacket> loginInfo)
        {
            // create the sendBuffer based on the message
            List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(loginInfo)));

            // append the message length (in bytes)
            sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

            // send the message
            this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
        }

        public void SendChatMessage(string message)
        {
            if (this.loggedIn)
            {
                DataPacket<ChatPacket> dataPacket = new DataPacket<ChatPacket>()
                {
                    sender = this.username,
                    type = "CHAT",
                    data = new ChatPacket()
                    {
                        receiver = clientUserName,
                        chatMessage = message
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void SendServerMessage(string receiver, string message)
        {
            if (this.loggedIn)
            {
                DataPacket<ChatPacket> dataPacket = new DataPacket<ChatPacket>()
                {
                    sender = this.username,
                    type = "SERVER_MESSAGE",
                    data = new ChatPacket()
                    {
                        receiver = clientUserName,
                        chatMessage = message
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void BroadCast(string message)
        {
            if (this.loggedIn)
            {
                DataPacket<ChatPacket> dataPacket = new DataPacket<ChatPacket>()
                {
                    sender = this.username,
                    type = "CHAT",
                    data = new ChatPacket()
                    {
                        receiver = "All",
                        chatMessage = message
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void SendResistance(int resistance)
        {
            if (this.loggedIn)
            {
                DataPacket<ResistancePacket> dataPacket = new DataPacket<ResistancePacket>()
                {
                    sender = this.username,
                    type = "RESISTANCE",
                    data = new ResistancePacket()
                    {
                        receiver = this.clientUserName,
                        resistance = resistance
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void StartSession()
        {
            if (this.loggedIn)
            {
                DataPacket<StartStopPacket> dataPacket = new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "START_SESSION",
                    data = new StartStopPacket()
                    {
                        receiver = this.clientUserName,
                        startSession = true
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void StopSession()
        {
            if (this.loggedIn)
            {
                DataPacket<StartStopPacket> dataPacket = new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "STOP_SESSION",
                    data = new StartStopPacket()
                    {
                        receiver = clientUserName,
                        startSession = false
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void EmergencyStopSession()
        {
            DataPacket<EmergencyStopPacket> dataPacket = new DataPacket<EmergencyStopPacket>()
            {
                sender = this.username,
                type = "EMERGENCY_STOP",
                data = new EmergencyStopPacket()
                {
                    receiver = clientUserName,
                    startSession = false,
                    resistance = 0
                }
            };

            // create the sendBuffer based on the message
            List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

            // append the message length (in bytes)
            sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

            // send the message
            this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
        }

        public void RequestClientList()
        {
            if (this.loggedIn)
            {
                DataPacket dataPacket = new DataPacket()
                {
                    sender = this.username,
                    type = "REQUEST_CLIENTLIST"
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void SendUserName(string userNameClient)
        {
            if (this.loggedIn)
            {
                this.clientUserName = userNameClient;
                DataPacket<UserNamePacket> dataPacket = new DataPacket<UserNamePacket>()
                {
                    sender = this.username,
                    type = "USERNAME",
                    data = new UserNamePacket()
                    {
                        clientUserName = userNameClient
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        public void OnCloseLiveSession()
        {
            if (this.loggedIn)
            {
                DataPacket<UserNamePacket> dataPacket = new DataPacket<UserNamePacket>()
                {
                    sender = this.username,
                    type = "DISCONNECT_LIVESESSION",
                    data = new UserNamePacket()
                    {
                        clientUserName = this.clientUserName
                    }
                };

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);

                this.clientUserName = null;


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

                        OnChatReceived?.Invoke(d.sender, $"{d.sender}: {d.data.chatMessage}\r\n");
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
                        OnSessionStateMessageReceived?.Invoke(d.sender,d.data.startSession);
                        break;
                    }
                case "INVALID_BIKE":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();
                        OnInvalidBikeReceived?.Invoke(d.sender);
                        break;
                    }
                default:
                    Console.WriteLine("Type is not valid");
                    break;
            }

        }
    }
}

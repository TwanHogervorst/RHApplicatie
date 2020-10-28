using Newtonsoft.Json;
using RHApplicationLib.Abstract;
using RHApplicationLib.Core;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace ClientApplication.Core
{

    public delegate void LoginCallback(bool status);
    public delegate void ChatCallback(string message);
    public delegate void ResistanceCallback(int resistance);
    public delegate void StartStopSessionCallback(bool state);
    public delegate void EmergencyStopSessionCallback(bool state, int resistance);

    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[4];
        public string username;
        private bool loggedIn = false;
        public string doctorUserName;

        public event LoginCallback OnLogin;
        public event ChatCallback OnChatReceived;
        public event ResistanceCallback OnResistanceReceived;
        public event StartStopSessionCallback OnStartStopSession;
        public event EmergencyStopSessionCallback OnEmergencyStopSession;

        public Client()
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
                    isClient = true,
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

        public void SendData(double speed, int heartbeat, double elapsedTime, int power, int distanceTraveled)
        {
            if (this.loggedIn)
            {
                DataPacket<BikeDataPacket> dataPacket = new DataPacket<BikeDataPacket>()
                {
                    sender = this.username,
                    type = "BIKEDATA",
                    data = new BikeDataPacket()
                    {
                        receiver = this.doctorUserName,
                        speed = speed,
                        heartbeat = heartbeat,
                        elapsedTime = elapsedTime,
                        power = power,
                        distanceTraveled = distanceTraveled,
                        timestamp = DateTime.Now
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
                        receiver = this.doctorUserName,
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

        public void SendStartStopSessionResponse(bool state)
        {
            if (this.loggedIn)
            {
                DataPacket<StartStopPacket> dataPacket = new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "SESSIONSTATE_RESPONSE",
                    data = new StartStopPacket()
                    {
                        receiver = this.doctorUserName,
                        startSession = state
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

        public void SendEmergencySessionResponse(bool _state)
        {
            if (this.loggedIn)
            {
                DataPacket<EmergencyResponsePacket> dataPacket = new DataPacket<EmergencyResponsePacket>()
                {
                    sender = this.username,
                    type = "SESSIONSTATE_EMERGENCYRESPONSE",
                    data = new EmergencyResponsePacket()
                    {
                        receiver = this.doctorUserName,
                        state = _state
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

        public void SendInvalidBike(bool state)
        {
            if (this.loggedIn)
            {
                DataPacket<StartStopPacket> dataPacket;

                if (state) {
                    dataPacket = new DataPacket<StartStopPacket>()
                    {
                        sender = this.username,
                        type = "INVALID_BIKE",
                        data = new StartStopPacket()
                        {
                            receiver = this.doctorUserName,
                            startSession = state
                        }
                    };
                }
                else
                {
                    dataPacket = new DataPacket<StartStopPacket>()
                    {
                        sender = this.username,
                        type = "INVALID_BIKE",
                        data = new StartStopPacket()
                        {
                            receiver = this.doctorUserName,
                            startSession = state
                        }
                    };
                }

                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dataPacket)));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        private void handleData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGINRESPONSE":
                    {
                        DataPacket<LoginResponse> d = data.GetData<LoginResponse>();
                        if (d.data.status == "OK" && d.data.isClient)
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
                        OnChatReceived?.Invoke($"{d.sender}: {d.data.chatMessage}\r\n");
                        break;
                    }
                case "USERNAME_RESPONSE":
                    {
                        DataPacket<UserNamePacketResponse> d = data.GetData<UserNamePacketResponse>();
                        this.doctorUserName = d.data.doctorUserName;
                        break;
                    }
                case "DISCONNECT_LIVESESSION":
                    {
                        this.doctorUserName = null;
                        break;
                    }
                case "RESISTANCE":
                    {
                        DataPacket<ResistancePacket> d = data.GetData<ResistancePacket>();
                        //TODO set Resistance of the bike
                        OnResistanceReceived?.Invoke(d.data.resistance);
                        break;
                    }
                case "START_SESSION":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();
                        OnStartStopSession?.Invoke(d.data.startSession);
                        break;
                    }
                case "STOP_SESSION":
                    {
                        DataPacket<StartStopPacket> d = data.GetData<StartStopPacket>();
                        OnStartStopSession?.Invoke(d.data.startSession);
                        break;
                    }
                case "EMERGENCY_STOP":
                    {
                        DataPacket<EmergencyStopPacket> d = data.GetData<EmergencyStopPacket>();
                        OnEmergencyStopSession?.Invoke(d.data.startSession, d.data.resistance);
                        break;
                    }
                case "SERVER_MESSAGE":
                    {
                        DataPacket<ChatPacket> d = data.GetData<ChatPacket>();
                        OnChatReceived?.Invoke(d.data.chatMessage);
                        break;
                    }
                default:
                    Console.WriteLine("Type is not valid");
                    break;
            }

        }
    }
}
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
using System.Windows.Forms;

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
        public string username;
        private bool loggedIn = false;
        public string doctorUserName;
        private bool isDisconnecting = false;

        public event LoginCallback OnLogin;
        public event ChatCallback OnChatReceived;
        public event ResistanceCallback OnResistanceReceived;
        public event StartStopSessionCallback OnStartStopSession;
        public event EmergencyStopSessionCallback OnEmergencyStopSession;

        private int receivedBytes;
        private byte[] receiveBuffer;

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
                    isClient = true,
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
            catch (System.Exception ex)
            {
                Disconnect();
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

                this.receiveBuffer = new byte[4];
                this.receivedBytes = 0;
                this.stream.BeginRead(this.receiveBuffer, 0, this.receiveBuffer.Length, new AsyncCallback(ReceiveLengthInt), null);
            }
            catch
            {
                Disconnect();
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

        public void SendData(double speed, int heartbeat, double elapsedTime, int power, double distanceTraveled, int resistance)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<BikeDataPacket>()
                {
                    sender = this.username,
                    type = "BIKEDATA",
                    data = new BikeDataPacket()
                    {
                        doctor = this.doctorUserName,
                        speed = speed,
                        heartbeat = heartbeat,
                        elapsedTime = elapsedTime,
                        power = power,
                        distanceTraveled = distanceTraveled,
                        resistance = resistance,
                        timestamp = DateTime.Now
                    }
                }.ToJson());
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
                        receiver = this.doctorUserName,
                        chatMessage = message,
                        isDoctorMessage = false
                    }
                }.ToJson());
            }
        }

        public void SendStartStopSessionResponse(bool state)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "SESSIONSTATE_RESPONSE",
                    data = new StartStopPacket()
                    {
                        receiver = this.doctorUserName,
                        startSession = state
                    }
                }.ToJson());
            }
        }

        public void SendEmergencySessionResponse(bool _state)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<EmergencyResponsePacket>()
                {
                    sender = this.username,
                    type = "SESSIONSTATE_EMERGENCYRESPONSE",
                    data = new EmergencyResponsePacket()
                    {
                        receiver = this.doctorUserName,
                        state = _state
                    }
                }.ToJson());
            }
        }

        public void SendInvalidBike(bool state)
        {
            if (this.loggedIn)
            {
                this.SendData(new DataPacket<StartStopPacket>()
                {
                    sender = this.username,
                    type = "INVALID_BIKE",
                    data = new StartStopPacket()
                    {
                        receiver = this.doctorUserName,
                        startSession = false
                    }
                }.ToJson());
            }
        }

        public void Disconnect()
        {
            if (!this.isDisconnecting) {
                this.isDisconnecting = true;
                if (this.client.Connected) {
                    //Send disconnect to server
                    MessageBox.Show("You are disconnected, this application is closing.", "Disconnect");
                    this.stream.Close();
                    this.client.Close();
                    Application.Exit();
                }
            }
        }

        public void DisconnectClient()
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
                        this.doctorUserName = d.data.doctor;
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
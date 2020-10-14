﻿
using Newtonsoft.Json;
using RHApplicationLib.Core;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace DoctorApplication_
{

    public delegate void LoginCallback(bool status);
    public delegate void ChatCallback(string message);

    public class DoctorClient
    {
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[4];
        private string username;
        private bool loggedIn = false;

        public event LoginCallback OnLogin;
        public event ChatCallback OnChatReceived;

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

        private void handleData(DataPacket data)
        {
            switch (data.type)
            {
                case "LOGINRESPONSE":
                    {
                        DataPacket<LoginResponse> d = data.GetData<LoginResponse>();
                        if (d.data.status == "OK")
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
                        OnChatReceived?.Invoke($"{d.sender}: {d.data.chatMessage}");
                        break;
                    }
                default:
                    Console.WriteLine("Type is not valid");
                    break;
            }

        }
    }
}
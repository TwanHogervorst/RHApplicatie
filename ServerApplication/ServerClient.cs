using Newtonsoft.Json;
using RHApplicationLib.Abstract;
using RHApplicationLib.Core;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.IO;
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
                    Console.WriteLine("Received a login packet");
                    DataPacket<LoginPacket> d = data.GetData<LoginPacket>();

                    if (Server.clientList.ContainsKey(d.data.username))
                    {
                        if (Server.clientList[d.data.username] == d.data.password)
                        {
                            SendData(new DataPacket<LoginResponse>()
                            {
                                sender = this.UserName,
                                type = "LOGINRESPONSE",
                                data = new LoginResponse()
                                {
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
                        //TODO send message to doctor-application
                        break;
                    }
                case "BIKEDATA":
                    {
                        Console.WriteLine("Received a bikeData packet");
                        DataPacket<BikeDataPacket> d = data.GetData<BikeDataPacket>();
                        //TODO save bikedata
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
    }
}

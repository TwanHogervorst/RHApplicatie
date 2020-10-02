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
    class ServerClient
    {
        private string password;
        private TcpClient client;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer;
        private string username;

        private bool loggedIn = false;

        public ServerClient()
        {
            
        }

        public void Connect(string username, string password)
        {
            if (!this.loggedIn)
            {
                this.username = username;
                this.password = password;

                this.client = new TcpClient();
                this.client.BeginConnect("localhost", 15243, new AsyncCallback(OnConnect), null);
                this.stream = client.GetStream();

                //Send username and password to check
                SendData(JsonConvert.SerializeObject(new DataPacket<LoginPacket>()
                {
                    type = "LOGIN",
                    data = new LoginPacket()
                    {
                        username = this.username,
                        password = this.password

                    }
                }));

                //Response on the check
                DataPacket<LoginResponse> result = ReveiveData<DataPacket<LoginResponse>>();
                
                //Check if response is OK
                if (result.type.Equals("LOGINRESPONSE") && result.data.status.Equals("OK"))
                {
                    this.loggedIn = true;
                    Console.WriteLine("You are logged in!");
                }
                else
                {
                    Console.WriteLine("Your username and/or password are incorrect!");
                }
            }

            //Loop where you are sending the chat messages
            while (this.loggedIn)
            {
                Console.WriteLine("Fill in your message here:");
                string newChatMessage = Console.ReadLine();
                SendData(JsonConvert.SerializeObject(newChatMessage));
            }
        }

        private void SendData(string message)
        {
            if (this.loggedIn)
            {
                // create the sendBuffer based on the message
                List<byte> sendBuffer = new List<byte>(Encoding.ASCII.GetBytes(message));

                // append the message length (in bytes)
                sendBuffer.InsertRange(0, Utility.ReverseIfBigEndian(BitConverter.GetBytes(sendBuffer.Count)));

                // send the message
                this.stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
            }
        }

        private T ReveiveData<T>() where T : DAbstract
        {
            return JsonConvert.DeserializeObject<T>(this.ReceiveData());
        }

        private string ReceiveData()
        {
            string result = null;

            // Get data length
            byte[] dataLengthBytes = new byte[4];
            this.stream.Read(dataLengthBytes, 0, 4);
            int dataLength = BitConverter.ToInt32(Utility.ReverseIfBigEndian(dataLengthBytes));

            // create data buffer
            byte[] dataBuffer = new byte[dataLength];

            // read data and fill buffer
            int bytesRead = 0;
            while (bytesRead < dataLength)
            {
                bytesRead += this.stream.Read(dataBuffer, bytesRead, dataLength - bytesRead);
            }

            // get message as string
            result = Encoding.ASCII.GetString(dataBuffer);

            Console.WriteLine(result);

            return result;
        }

        private void OnConnect(IAsyncResult ar)
        {
            client.EndConnect(ar);
            Console.WriteLine("Connected!");
            stream = client.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
            write($"login\r\n{username}\r\n{password}");
        }

        private void OnRead(IAsyncResult ar)
        {
            int receivedBytes = stream.EndRead(ar);
            string receivedText = System.Text.Encoding.ASCII.GetString(buffer, 0, receivedBytes);
            totalBuffer += receivedText;

            while (totalBuffer.Contains("\r\n\r\n"))
            {
                string packet = totalBuffer.Substring(0, totalBuffer.IndexOf("\r\n\r\n"));
                totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("\r\n\r\n") + 4);
                string[] packetData = Regex.Split(packet, "\r\n");
                handleData(packetData);
            }
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(OnRead), null);
        }
        private void write(string data)
        {
            var dataAsBytes = System.Text.Encoding.ASCII.GetBytes(data + "\r\n\r\n");
            stream.Write(dataAsBytes, 0, dataAsBytes.Length);
            stream.Flush();
        }

        private void handleData(string[] packetData)
        {
            Console.WriteLine($"Packet ontvangen: {packetData[0]}");

            switch (packetData[0])
            {
                case "login":
                    if (packetData[1] == "ok")
                    {
                        Console.WriteLine("Logged in!");
                        loggedIn = true;
                    }
                    else
                        Console.WriteLine(packetData[1]);
                    break;
                case "chat":
                    Console.WriteLine($"Chat received: '{packetData[1]}'");
                    break;
            }

        }
    }
}
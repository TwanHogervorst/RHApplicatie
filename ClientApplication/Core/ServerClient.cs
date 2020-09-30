using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
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
        private Dictionary<string, string> clientList = new Dictionary<string, string>();

        private bool loggedIn = false;

        public ServerClient()
        {
            this.clientList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ClientList.txt"));
        }

        public void Connect(string username, string password)
        {
            if (!loggedIn)
            {

                this.username = username;
                this.password = password;

                if (this.clientList.ContainsKey(this.username) && this.clientList[this.username] == this.password)
                {
                    client = new TcpClient();
                    client.BeginConnect("localhost", 15243, new AsyncCallback(OnConnect), null);
                    this.loggedIn = true;
                    while (true)
                    {
                        Console.WriteLine("Fill in your message here:");
                        string newChatMessage = Console.ReadLine();
                        if (loggedIn)
                            write($"chat\r\n{newChatMessage}");
                        else
                            Console.WriteLine("You have not been logged in yet");
                    }
                }

            }
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
﻿using RHApplicationLib.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace ServerApplication
{
    class Client
    {
        private TcpClient tcpClient;
        private NetworkStream stream;
        private byte[] buffer = new byte[1024];
        private string totalBuffer = "";
        private bool isConnected;

        public string UserName { get; set; }
        public string PassWord { get; set; }

        public Client(TcpClient tcpClient)
        {
            isConnected = true;
            this.tcpClient = tcpClient;

            this.stream = this.tcpClient.GetStream();
            stream.BeginRead(buffer, 0, buffer.Length, new AsyncCallback(ReceiveData), null);
        }

        private void ReceiveData(IAsyncResult ar)
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
        }

        //private void OnRead(IAsyncResult ar)
        //{
        //    try
        //    {
        //        int receivedBytes = stream.EndRead(ar);
        //        string receivedText = ""; //TODO ??
        //        totalBuffer += receivedText;
        //    }
        //    catch (IOException)
        //    {
        //        Program.Disconnect(this);
        //        return;
        //    }

        //    while (totalBuffer.Contains("iets")) //TODO beslissen wat voor 'endkey'
        //    {
        //        string packet = totalBuffer.Length
        //        totalBuffer = totalBuffer.Substring(totalBuffer.IndexOf("iets"));
        //        string[] packetData = Regex.Split(packet, "nogiets");
        //        handleData(packetData);
        //    }


        //}


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


      




        private void handleData(string[] packetData)
        {
            Console.WriteLine($"Got a packet: {packetData}");
        }

        public void Write(string data)
        {
            
        }
    }
}
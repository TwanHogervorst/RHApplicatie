using NESessionList.Data;
using NESessionList.Exception;
using Newtonsoft.Json;
using RHApplicationLib.Abstract;
using RHApplicationLib.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

namespace NESessionList.Core
{
    public class VRClient
    {

        private string ipAddress;
        private ushort port; // Port number cant be bigger than 65535, so type = ushort

        private TcpClient client;
        private NetworkStream stream;

        public bool IsConnected => this.client != null && this.client.Connected;

        public VRClient(string ipAddress, ushort port)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress temp)) new ArgumentException($"IP address is invalid", nameof(ipAddress));

            this.ipAddress = ipAddress;
            this.port = port;
        }

        public bool Connect()
        {
            bool result = false;

            try
            {
                if(!this.IsConnected)
                {
                    this.client = new TcpClient(this.ipAddress, this.port);
                    this.stream = this.client.GetStream();
                }

                result = this.IsConnected;
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine($"VRClient => {ex.GetType().Name}: {ex.Message}");
                result = false;
            }

            return result;
        }

        public List<DVRSessionItem> GetSessionList()
        {
            if(!this.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            this.SendData("session/list", null);

            DVRClientPacketArrayResponse<DVRSessionItem> responseObj = this.ReveiveData<DVRClientPacketArrayResponse<DVRSessionItem>>();

            return responseObj.data;
        }

        public VRTunnel CreateTunnel(string sessionId, string key = "")
        {
            VRTunnel result = null;

            this.SendData("tunnel/create", new DVRTunnelCreate() { session = sessionId, key = key });
            DVRClientPacket<DVRClientReceivePacket> tunnelIdPacket = this.ReveiveData<DVRClientPacket<DVRClientReceivePacket>>();

            if (tunnelIdPacket.data.status == "ok") result = new VRTunnel(tunnelIdPacket.data.id, this);
            else throw new VRClientException(tunnelIdPacket.data.msg);

            return result;
        }

        internal DVRTunnelReceivePacket<T> SendAndReceiveData<T>(DVRTunnelPacket data) where T : DAbstract
        {
            DVRTunnelReceivePacket<T> result = null;

            this.SendData(data);
            // unpack VRClientPacket
            result = this.ReveiveData<DVRClientPacket<DVRTunnelReceivePacket<T>>>().data;

            return result;
        }

        internal void SendData(DVRTunnelPacket data)
        {
            this.SendData("tunnel/send", data); 
        }

        private void SendData(string id, DAbstract data = null) =>
            this.SendData(new DVRClientPacket<DAbstract>() { id = id, data = data }.ToJson()); // Create VRClientPacket

        private void SendData(string message)
        {
            if(this.IsConnected)
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
            while(bytesRead < dataLength)
            {
                bytesRead += this.stream.Read(dataBuffer, bytesRead, dataLength - bytesRead);
            }

            // get message as string
            result = Encoding.ASCII.GetString(dataBuffer);

            Console.WriteLine(result);
            
            return result;
        }
    }
}

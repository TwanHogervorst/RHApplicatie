using Microsoft.VisualBasic.CompilerServices;
using Newtonsoft.Json;
using RHApplicationLib.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace NESessionList
{
    class VRClient
    {
        private string IPAdress;
        private int port;
        private string serverID;
        private string tunnelID;
        private NetworkStream stream;

        public VRClient(string iPAdress, int port)
        {
            this.IPAdress = iPAdress;
            this.port = port;
            this.serverID = String.Empty;
            this.tunnelID = String.Empty;

            Connect(this.IPAdress);


        }

        public void Connect(String server)
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 6666;
                TcpClient client = new TcpClient(server, port);
                this.stream = client.GetStream();

                sendData("{\"id\" : \"session/list\"}");

                string responseString = receiveData();

                dynamic inputData = JsonConvert.DeserializeObject(responseString);

                //Console.WriteLine("ServerID: " + inputData.data[0].id);

                this.serverID = inputData.data[0].id;
                Console.WriteLine(this.serverID);

                sendData("{\"id\":\"tunnel/create\",\"data\":{\"session\":\"" + this.serverID + "\",\"key\":\"\"}}");
                responseString = receiveData();
                inputData = JsonConvert.DeserializeObject(responseString);

                this.tunnelID = inputData.data.id;
                Console.WriteLine(this.tunnelID);

                sendData("{\"id\":\"tunnel/send\",\"data\":{\"dest\":\"" + this.tunnelID + "\",\"data\":{\"id\":\"scene/reset\",\"data\":{}}}}");
                responseString = receiveData();
                inputData = JsonConvert.DeserializeObject(responseString);

                Console.WriteLine("DONE");
                // Close everything.
                stream.Close();
                client.Close();
            }

            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }

        public void sendData(string message)
        {
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

            // Get a client stream for reading and writing.
            //  Stream stream = client.GetStream();


            List<byte> dataList = new List<byte>(data);

            dataList.InsertRange(0, BitConverter.GetBytes(dataList.Count));

            Byte[] byteMessage = dataList.ToArray();
            // Send the message to the connected TcpServer.
            this.stream.Write(byteMessage, 0, byteMessage.Length);

            Console.WriteLine("Sent: {0}", byteMessage.ToString());
        }

        public string receiveData()
        {
            byte[] dataLengthBytes = new byte[4];

            this.stream.Read(dataLengthBytes, 0, dataLengthBytes.Length);
            int dataLength = BitConverter.ToInt32(dataLengthBytes);

            foreach (byte bit in dataLengthBytes)
            {
                Console.WriteLine(bit);
            }

            byte[] dataBuffer = new byte[dataLength];
            int bytesRead = 0;

            while (bytesRead < dataLength)
            {
                bytesRead += this.stream.Read(dataBuffer, Math.Max(0, bytesRead - 1), dataBuffer.Length - bytesRead);
            }

            string responseString = Encoding.ASCII.GetString(dataBuffer);

            Console.WriteLine(responseString);

            return responseString;
        }

        public void AddNode(string name, int xPos, int yPos, int zPos, int scale, int xRot, int yRot, int zRot,
                            string filename, int width, int height, int panelWidth, int panelHeight, bool cullbackfaces = true, bool animated = false,
                            string animationName = null, bool smoothnormals = true, int resolutionX = 512, int resolutionY = 512,
                            bool castShadow = true)
        {
            sendData(
                "{\"id\":\"scene/node/add\",\"data\":{\"name\":\"" + name + "\",\"parent\":\"guid\",\"components\"" + ":{\"transform\":{\"position\"" +
                ":[" + xPos + "," + yPos + "," + zPos + "],\"scale\":" + scale + ",\"rotation\":" + "[" + xRot + "," + yRot + "," + zRot + "]},\"model\"" +
                ":{\"file\"" + ":\"" + filename + "\",\"cullbackfaces\":" + cullbackfaces + ",\"animated\":" + animated + ",\"animation\":\"" + animationName + "\"},\"terrain\"" + "" +
                ":{\"smoothnormals\":" + smoothnormals + "},\"panel\":{\"size\":[" + panelWidth + "," + panelHeight + "],\"resolution\":[" + resolutionX + "," + resolutionY + "],\"background\"" +
                ":[1,1,1,1]," + "\"castShadow\":" + castShadow + "}}}}");


            //TODO: save name and parent in attribute
        }

        public void AddNode(string name, int xPos, int yPos, int zPos, int scale, int xRot, int yRot, int zRot,
                            string filename, bool cullbackfaces = true, bool animated = false,
                            string animationName = null, bool smoothnormals = true)
        {
            sendData(
                "{\"id\":\"scene/node/add\",\"data\":{\"name\":\"" + name + "\",\"parent\":\"guid\",\"components\"" + ":{\"transform\":{\"position\"" +
                ":[" + xPos + "," + yPos + "," + zPos + "],\"scale\":" + scale + ",\"rotation\":" + "[" + xRot + "," + yRot + "," + zRot + "]},\"model\"" +
                ":{\"file\"" + ":\"" + filename + "\",\"cullbackfaces\":" + cullbackfaces + ",\"animated\":" + animated + ",\"animation\":\"" + animationName + "\"},\"terrain\"" + "" +
                ":{\"smoothnormals\":" + smoothnormals + "}}}}");

            //TODO: save name and parent in attribute
        }


    }

}

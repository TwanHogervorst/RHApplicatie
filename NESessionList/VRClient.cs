using Microsoft.VisualBasic.CompilerServices;
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

        public VRClient(string iPAdress, int port)
        {
            this.IPAdress = iPAdress;
            this.port = port;

            Connect(this.IPAdress, "{\"id\" : \"session/list\"}");
        }

        //public void Connect(string IPAdress, int port)
        //{
        //    TcpClient client = new TcpClient();
        //    client.Connect(IPAdress, port);
        //    NetworkStream stream = client.GetStream();


        //}
        public static void Connect(String server, String message)

        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 6666;
                TcpClient client = new TcpClient(server, port);

                // Translate the passed message into ASCII and store it as a Byte array.
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                // Get a client stream for reading and writing.
                //  Stream stream = client.GetStream();

                NetworkStream stream = client.GetStream();

                List<byte> dataList = new List<byte>(data);

                dataList.InsertRange(0, BitConverter.GetBytes(dataList.Count));

                Byte[] byteMessage = dataList.ToArray() ;
                // Send the message to the connected TcpServer.
                stream.Write(byteMessage, 0, byteMessage.Length);

                Console.WriteLine("Sent: {0}", byteMessage.ToString());

                // Receive the TcpServer.response.

                // Buffer to store the response bytes.
                data = new Byte[4];
                stream.Read(data, 0, data.Length);

                Byte[] input = new Byte[BitConverter.ToInt32(data)*10];

                stream.Read(input, 0, input.Length);
                String responseData = String.Empty;

                responseData = System.Text.Encoding.ASCII.GetString(input);

                Console.WriteLine("Received: {0}", responseData);
                Console.WriteLine(BitConverter.ToInt32(data));

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

        public void sendData()
        {

        }

        public string receiveData()
        {
            return "";
        }
    }
}

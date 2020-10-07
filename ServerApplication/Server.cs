using Newtonsoft.Json;
using ServerUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApplication
{
    class Server
    {

        private TcpListener listener;
        private ClientDatabase clients;
        public static Dictionary<string, string> clientList;
        public static Dictionary<string, List<BikeDataPacket>> clientData;

        public Server()
        {
            Console.WriteLine("Hello Server!");
            clientList = new Dictionary<string, string>();
            clientList.Add("test", "test");

            clientData = new Dictionary<string, List<BikeDataPacket>>();
            string pathToClientData = "ClientData.txt";
            string contentClientData = JsonConvert.SerializeObject(clientData);
            File.WriteAllText(pathToClientData, contentClientData);

            this.clients = new ClientDatabase();

            string pathToClientList = "ClientList.txt";
            string contentClientList = JsonConvert.SerializeObject(clientList);
            File.WriteAllText(pathToClientList, contentClientList);

            this.listener = new TcpListener(IPAddress.Any, 15243);
            this.listener.Start();
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private void OnConnect(IAsyncResult ar)
        {
            clientList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ClientList.txt"));

            var tcpClient = this.listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            this.clients.AddClient(tcpClient);
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        internal void Disconnect(ServerClient client)
        {
            this.clients.RemoveClient(client);
            Console.WriteLine("Client disconnected");
        }

        internal void SendToUser(string user, DataPacket packet)
        {
            foreach (var client in this.clients.GetClients().Where(c => c.UserName == user))
            {
                client.SendData(packet);
            }
        }
    }
}

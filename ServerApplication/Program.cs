using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace ServerApplication
{
    class Program
    {

        private static TcpListener listener;
        private static List<Client> clients = new List<Client>();
        private static Dictionary<string, string> clientList = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Server!");

            clientList.Add("test", "test");
            string path = "ClientList.txt";
            string content = JsonConvert.SerializeObject(clientList);
            File.WriteAllText(path, content); 

            listener = new TcpListener(IPAddress.Any, 15243);
            listener.Start();
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);

            Console.ReadLine();
        }

        private static void OnConnect(IAsyncResult ar)
        {
            clientList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ClientList.txt"));


            var tcpClient = listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            clients.Add(new Client(tcpClient));
            listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }



        internal static void Disconnect(Client client)
        {
            clients.Remove(client);
            Console.WriteLine("Client disconnected");
        }

        internal static void SendToUser(string user, string packet)
        {
            foreach (var client in clients.Where(c => c.UserName == user))
            {
                client.Write(packet);
            }
        }
    }
}

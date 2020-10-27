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
        public static ClientDatabase tempList;
        public static ClientDatabase clients;
        public static ClientDatabase doctors;
        public static Dictionary<string, string> clientList;
        public static Dictionary<string, string> doctorList;
        public static Dictionary<string, List<BikeDataPacket>> clientData;
        public static Dictionary<string, List<BikeDataPacket>> doctorData;

        public Server()
        {
            Console.WriteLine("Hello Server!");
            clientList = new Dictionary<string, string>();
            clientList.Add("test", "test");
            clientList.Add("test2", "test2");
            clientList.Add("test3", "test3");

            doctorList = new Dictionary<string, string>();
            doctorList.Add("test1", "test1");

            clientData = new Dictionary<string, List<BikeDataPacket>>();
            string pathToClientData = "ClientData.txt";
            string contentClientData = JsonConvert.SerializeObject(clientData);
            File.WriteAllText(pathToClientData, contentClientData);

            doctorData = new Dictionary<string, List<BikeDataPacket>>();
            string pathToDoctorData = "DoctorData.txt";
            string contentDoctorData = JsonConvert.SerializeObject(doctorData);
            File.WriteAllText(pathToDoctorData, contentDoctorData);

            clients = new ClientDatabase();
            doctors = new ClientDatabase();
            tempList = new ClientDatabase();

            string pathToClientList = "ClientList.txt";
            string contentClientList = JsonConvert.SerializeObject(clientList);
            File.WriteAllText(pathToClientList, contentClientList);

            string pathToDoctorList = "DoctorList.txt";
            string contentDoctorList = JsonConvert.SerializeObject(doctorList);
            File.WriteAllText(pathToDoctorList, contentDoctorList);

            this.listener = new TcpListener(IPAddress.Any, 15243);
            this.listener.Start();
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        private void OnConnect(IAsyncResult ar)
        {
            clientList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("ClientList.txt"));
            doctorList = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("DoctorList.txt"));

            var tcpClient = this.listener.EndAcceptTcpClient(ar);
            Console.WriteLine($"Client connected from {tcpClient.Client.RemoteEndPoint}");
            tempList.AddClient(tcpClient);
            this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
        }

        internal void Disconnect(ServerClient client)
        {
            if (clients.GetClients().Contains(client))
            {
                clients.RemoveClient(client);
                Console.WriteLine("Client disconnected");

                Dictionary<string, bool> temp = new Dictionary<string, bool>();
                foreach (string userName in Server.clientList.Keys)
                {
                    temp.Add(userName, Server.clients.GetClients().FirstOrDefault(client => client.UserName == userName) != null);
                }

                DataPacket<ClientListPacket> activeClients = new DataPacket<ClientListPacket>()
                {
                    sender = client.UserName,
                    type = "RESPONSE_CLIENTLIST",
                    data = new ClientListPacket()
                    {
                        clientList = temp
                    }

                };

                foreach (ServerClient doctor in Server.doctors.GetClients())
                {
                    client.SendDataToUser(doctor, activeClients);
                }
            }
            else if (doctors.GetClients().Contains(client))
            {
                doctors.RemoveClient(client);
                Console.WriteLine("Doctor disconnected");
            }
        }

        internal void SendToUser(string user, DataPacket packet)
        {
            foreach (var client in clients.GetClients().Where(c => c.UserName == user))
            {
                client.SendData(packet);
            }
        }

        internal void Broadcast(DataPacket packet)
        {
            foreach (var client in clients.GetClients())
            {
                client.SendData(packet);
            }
        }
    }
}

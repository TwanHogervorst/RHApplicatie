using Newtonsoft.Json;
using RHApplicatieLib;
using RHApplicatieLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerApplication.Core
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
            clientList.Add("client1", "client1");
            clientList.Add("client2", "letmein");
            clientList.Add("client3", "p@ssw0rd");
            clientList.Add("client4", "f#ckD0ct3r$");
            clientList.Add("client5", "w@chtw00rd");
            clientList.Add("henk", "henkhenk2");

            doctorList = new Dictionary<string, string>();
            doctorList.Add("doctor1", "doctor1");
            doctorList.Add("doctor2", "st3th3r");
            doctorList.Add("doctor3", "ihatemyjob");
            doctorList.Add("doctor4", "mriscan69");

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
            try
            {
                this.listener.BeginAcceptTcpClient(new AsyncCallback(OnConnect), null);
            }
            catch (Exception ex)
            {
                foreach (ServerClient client in clients.GetClients()) {
                    Disconnect(client);
                }
                foreach (ServerClient doctor in doctors.GetClients())
                {
                    Disconnect(doctor);
                }
                Environment.Exit(0);
            }
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

        public static void Disconnect(ServerClient client)
        {
            if (clients.GetClients().Contains(client))
            {
                client.SendDataToUser(client,new DataPacket<DisconnectRequestPacket>()
                {
                    sender = "Server",
                    type = "DISCONNECT_REQUEST",
                    data = new DisconnectRequestPacket()
                    {
                    }
                }.ToJson());
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
                client.SendDataToUser(client, new DataPacket<DisconnectRequestPacket>()
                {
                    sender = "Server",
                    type = "DISCONNECT_REQUEST",
                    data = new DisconnectRequestPacket()
                    {
                    }
                }.ToJson());
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

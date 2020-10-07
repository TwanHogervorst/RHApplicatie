using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerApplication
{
    class ClientDatabase
    {

        private List<ServerClient> clients = new List<ServerClient>();

        public ClientDatabase()
        {
            this.clients = new List<ServerClient>();
        }

        public List<ServerClient> GetClients()
        {
            return this.clients;
        }


        public void AddClient(TcpClient client)
        {
            this.clients.Add(new ServerClient(client));
        }

        public void RemoveClient(ServerClient client)
        {
            this.clients.Remove(client);
        }
    }
}

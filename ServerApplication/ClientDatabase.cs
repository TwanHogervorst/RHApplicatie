using System;
using System.Collections.Generic;
using System.Text;

namespace ServerApplication
{
    class ClientDatabase
    {

        private List<Client> clients = new List<Client>();

        public List<Client> getClients()
        {
            return this.clients;
        }


        public void AddClient(Client client)
        {
            this.clients.Add(client);
        }

    }
}

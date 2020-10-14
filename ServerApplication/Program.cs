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
        static void Main(string[] args)
        {
            Server server = new Server();

            Console.ReadLine();
        }
    }
}

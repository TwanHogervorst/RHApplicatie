using NESessionList.Core;
using NESessionList.Data;
using NESessionList.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace NESessionList
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/

            VRClient client = new VRClient("145.48.6.10", 6666);
            if(client.Connect())
            {
                List<DVRSessionItem> sessionList = client.GetSessionList();

                for(int i = 0; i < sessionList.Count; i++)
                {
                    Console.WriteLine($"{i+1} {sessionList[i].clientinfo.user}");
                }

                Console.Write("Select Session: ");

                int selectedSession;
                while (!int.TryParse(Console.ReadLine(), out selectedSession)) Console.Write("Select Session: ");

                Console.Write("Enter key (leave empty if not needed): ");
                string key = Console.ReadLine();

                try
                {
                    VRTunnel tunnel = client.CreateTunnel(sessionList[selectedSession - 1].id, key);

                    if (tunnel != null) tunnel.AddNode();
                }
                catch (VRClientException ex)
                {
                    Console.WriteLine($"Create Tunnel failed: {ex.Message}");
                }
                
            }
            else
            {
                Console.WriteLine("Connection Failed!");
            }
            
        }
    }
}

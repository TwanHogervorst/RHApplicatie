using NESessionList.Core;
using NESessionList.Data;
using NESessionList.Exception;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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

                    if (tunnel != null)
                    {
                        //tunnel.SetTimeSkyBox(new decimal(20));
                        //tunnel.AddTerrain(new decimal[] {512,512}, new decimal[] {0});
                        //tunnel.AddNode(new DVRAddNodePacket.DComponents.DTransform(new decimal[] {0,5,10}, new decimal(1),new decimal[]{0,0,0}),
                        //new DVRAddNodePacket.DComponents.DModel("adesert_cracks_d.jpg",false,false,null), null, null, new DVRAddNodePacket.DComponents.DWater(new decimal[] { 1,1},new decimal(0.1)));




                        //tunnel.AddTerrain(new decimal[] { 10, 10 }, new decimal[10 * 10]);

                        //tunnel.AddNodeLayer("0", "data/NetworkEngine/textures/tarmac_normal.png", "data/NetworkEngine/textures/tarmac_normal.png",
                        //   -5, 10, 100);


                        //tunnel.AddNode(new DVRAddNodePacket.DComponents.DTransform(new decimal[] { 0, 0, 0 }, new decimal(1), new decimal[] { 0, 0, 0 }),
                        //                null,
                        //                new DVRAddNodePacket.DComponents.DTerrain(true),
                        //                null,
                        //                null);

                        
                        tunnel.FindNode("GroundPlane");
                        //tunnel.GetScene();

                    }
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

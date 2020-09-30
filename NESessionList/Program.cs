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
                        tunnel.SceneReset();
                        tunnel.AddTerrain(new decimal[] { 256,256 }, new decimal[256*256]);
                        //tunnel.SetTimeSkyBox(new decimal(15));
                        tunnel.AddNode("terrain", new DVRAddNodePacket.DComponents.DTransform(new decimal[] { 0, 0, 0 }, new decimal(1), new decimal[] { 0, 0, 0 }),
                                        null,
                                        new DVRAddNodePacket.DComponents.DTerrain(true),
                                        null,
                                        null);
                        //tunnel.DeleteTerrain();

                        tunnel.AddNode("tree1", new DVRAddNodePacket.DComponents.DTransform(new decimal[] { 0, 5, 0 }, new decimal(1), new decimal[] { 0, 0, 0 }),
                                        new DVRAddNodePacket.DComponents.DModel("data/NetworkEngine/models/trees/fantasy/tree1.obj", false, false, null),
                                        null,
                                        null,
                                        null);
                        //tunnel.DeleteNode(tunnel.nodeList["tree1"]);

                        //tunnel.UpdateNode(tunnel.nodeList["tree1"], new DVRUpdateNodePacket.DTransform(new decimal[] { 10, 0, 0 },10,new decimal[]{0,0,0 }),null);
                        //tunnel.MoveNodeTo(tunnel.nodeList["tree1"], new decimal[] { 100, 0, 0 }, "XY", "linear", true, new decimal(0), new decimal(5));
                        //tunnel.MoveNodeTo(tunnel.nodeList["tree1"], new decimal[] { 100, 0, 0 }, "XY", "linear", true, new decimal(10));


                        tunnel.ShowRoute(true);
                        tunnel.AddRoute("route test", new List<DVRAddRouteNodesPacket> {
                            new DVRAddRouteNodesPacket(new decimal[] { 0,10,0},new decimal[] { 5,0,-5}),
                            new DVRAddRouteNodesPacket(new decimal[] { 50, 10, 0 }, new decimal[] { 5, 0, 5 }),
                            new DVRAddRouteNodesPacket(new decimal[] { 50, 10, 50 }, new decimal[] { -5, 0, 5 }),
                            new DVRAddRouteNodesPacket(new decimal[] { 0, 10, 50 }, new decimal[] { -5, 0, -5 }) });
                        tunnel.AddRoad(
                            tunnel.routeList["route test"],
                            "data/NetworkEngine/textures/tarmac_diffuse.png",
                            "data/NetworkEngine/textures/tarmac_normal.png",
                            "data/NetworkEngine/textures/tarmac_specular.png",
                            new decimal(0.01));

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

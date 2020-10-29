using RHApplicatieLib.Core;
using RHApplicatieLib.Data;
using RHApplicatieLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading;

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
            if (client.Connect())
            {
                List<DVRSessionItem> sessionList = client.GetSessionList();
                for (int i = 0; i < sessionList.Count; i++)
                {
                    Console.WriteLine($"{i + 1} {sessionList[i].clientinfo.user}");
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
                        bool succeded = false;
                        while (!succeded)
                        {
                            try
                            {
                                succeded = SetScene(tunnel);
                            }
                            catch (VRCallbackException ex)
                            {
                                Console.WriteLine($"Error package: {ex.Message}");
                            }
                        }
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

        public static bool SetScene(VRTunnel tunnel)
        {
            tunnel.SceneReset();

            tunnel.FindNode("GroundPlane");
            tunnel.DeleteNode(tunnel.nodeList["GroundPlane"]);

            tunnel.SetTimeSkyBox(15m);

            tunnel.AddTerrain(new decimal[] { 256, 256 }, new decimal[256 * 256]);
            tunnel.AddNode("terrain", new DVRAddNodePacket.DComponents.DTransform(new decimal[] { -128, 0, -128 }, new decimal(1), new decimal[] { 0, 0, 0 }),
                           null,
                           new DVRAddNodePacket.DComponents.DTerrain(true),
                           null,
                           null);

            //ADD TEXTURE TO TERRAIN (DOESNT WORK)
            //tunnel.AddNodeLayer(tunnel.nodeList["terrain"], 
            //    "data/NetworkEngine/textures/terrain/grass_mix_d.jpg", 
            //    "data/NetworkEngine/textures/grass_mix_n.jpg", 
            //    new decimal(0),
            //    new decimal(100),new decimal(1));
            //tunnel.UpdateTerrain();
            //tunnel.UpdateNode(tunnel.nodeList["terrain"]);

            //tunnel.FindNode("Head");
            //tunnel.MoveNodeTo(tunnel.nodeList["Head"],new decimal[] { 100,0,100},"XY","linear",false,10);
            //tunnel.UpdateNode(tunnel.nodeList["Head"]);

            tunnel.AddNode("bike", new DVRAddNodePacket.DComponents.DTransform(new decimal[] { 0, 5, 0 }, new decimal(0.01), new decimal[] { 0, 0, 0 }),
                           new DVRAddNodePacket.DComponents.DModel("data/NetworkEngine/models/bike/bike_anim.fbx", false, true, "Armature|Fietsen"),
                            null,
                            null,
                            null);
            tunnel.UpdateNode(tunnel.nodeList["bike"], null, new DVRUpdateNodePacket.DAnimation("Armature|Fietsen", new decimal(1)));


            int sizeOfGrid = 8;
            Random r = new Random();
            for (int i = 0; i < sizeOfGrid; i++)
            {
                for (int j = 0; j < sizeOfGrid; j++)
                {
                    tunnel.AddNode($"tree{(i * sizeOfGrid) + j}", new DVRAddNodePacket.DComponents.DTransform(new decimal[] { i * 5 + 5 + r.Next(0, 5), 0, j * 5 + 5 }, new decimal(r.Next(2, 4)), new decimal[] { 0, 0, 0 }),
                           new DVRAddNodePacket.DComponents.DModel("data/NetworkEngine/models/trees/fantasy/tree4.obj", false, false, null),
                            null,
                            null,
                            null);
                }
            }

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
            tunnel.FollowRoute(tunnel.routeList["route test"],
                               tunnel.nodeList["bike"],
                               new decimal(1),
                               new decimal(0), "XZ",
                               new decimal(1),
                               true,
                               new decimal[] { 0, 0, 0 },
                               new decimal[] { 0, 0, 0 });
            tunnel.FollowRouteSpeed(tunnel.nodeList["bike"], new decimal(1));

            tunnel.AddNode("water1",
                new DVRAddNodePacket.DComponents.DTransform
                (new decimal[] { 65, 1, -10 }, new decimal(1), new decimal[] { 0, 0, 0 })
                , null,
                null,
                null,
                new DVRAddNodePacket.DComponents.DWater(new decimal[] { 160, 10 }, new decimal(0.1)));
            tunnel.AddNode("water2",
                new DVRAddNodePacket.DComponents.DTransform
                (new decimal[] { 65, 1, -10 }, new decimal(1), new decimal[] { 0, 90, 0 })
                , null,
                null,
                null,
                new DVRAddNodePacket.DComponents.DWater(new decimal[] { 160, 10 }, new decimal(0.1)));
            tunnel.AddNode("water3",
                new DVRAddNodePacket.DComponents.DTransform
                (new decimal[] { -10, 1, 65 }, new decimal(1), new decimal[] { 0, -90, 0 })
                , null,
                null,
                null,
                new DVRAddNodePacket.DComponents.DWater(new decimal[] { 160, 10 }, new decimal(0.1)));
            tunnel.AddNode("water4",
                new DVRAddNodePacket.DComponents.DTransform
                (new decimal[] { -10, 1, 65 }, new decimal(1), new decimal[] { 0, -180, 0 })
                , null,
                null,
                null,
                new DVRAddNodePacket.DComponents.DWater(new decimal[] { 160, 10 }, new decimal(0.1)));

            tunnel.AddNode("panel", new DVRAddNodePacket.DComponents.DTransform(new decimal[] { 25, 20, 70 }, new decimal(1), new decimal[] { 0, 180, 0 }),
                            null,
                            null,
                            new DVRAddNodePacket.DComponents.DPanel(new decimal[] { 50, 20 }, new decimal[] { 512, 512 }, new decimal[] { 1, 1, 1, 1 }, false),
                            null);
            tunnel.ClearPanel(tunnel.nodeList["panel"]);
            tunnel.SwapPanel(tunnel.nodeList["panel"]);
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Bikedata", new decimal[] { 200, 50 }, new decimal(50), new decimal[] { 255, 0, 0, 1 });
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Current Speed: value", new decimal[] { 25, 100 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Current Heartbeat: value", new decimal[] { 25, 150 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Elapsed Time: value", new decimal[] { 25, 200 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Distance traveled: value", new decimal[] { 25, 250 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Power: value", new decimal[] { 25, 300 }, new decimal(50));
            tunnel.DrawTextPanel(tunnel.nodeList["panel"], "Resistance: value", new decimal[] { 25, 350 }, new decimal(50));
            tunnel.SwapPanel(tunnel.nodeList["panel"]);

            return true;
        }
    }
}

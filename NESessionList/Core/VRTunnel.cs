using NESessionList.Data;
using NESessionList.Exception;
using RHApplicationLib.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace NESessionList.Core
{
    public class VRTunnel
    {
        private string tunnelId;
        private VRClient client;

        public VRTunnel(string tunnelId, VRClient client)
        {
            this.tunnelId = tunnelId;
            this.client = client;
        }

        public void AddNode(DVRAddNodePacket.DComponents.DTransform transform_, DVRAddNodePacket.DComponents.DModel model_,
                            DVRAddNodePacket.DComponents.DTerrain terrain_, DVRAddNodePacket.DComponents.DPanel panel_, DVRAddNodePacket.DComponents.DWater water_  )
        {
            try
            {
                DVRClientPacket<DVRAddNodeResult> result = this.SendAndReceiveData<DVRClientPacket<DVRAddNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/add",
                    data = new DVRAddNodePacket() // Create Add Node Packet
                    {
                        name = "Test",
                        components = new DVRAddNodePacket.DComponents
                        {
                            transform = transform_,
                            model = model_,
                            terrain = terrain_,
                            panel = panel_,
                            water = water_
                        }
                    }
                }) ; ;

                if (result != null) Console.WriteLine($"Added node: {result.data.name} (uuid: {result.data.uuid})");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add Node failed: {ex.Message}");
            }
        }

        public void UpdateNode(DVRUpdateNodePacket.DTransform transform_,DVRUpdateNodePacket.DAnimation animation_)
        {
            try
            {
                DVRClientPacket<DVRUpdateNodeResult> result = this.SendAndReceiveData<DVRClientPacket<DVRUpdateNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/update",
                    data = new DVRUpdateNodePacket() // Create Add Node Packet
                    {
                        id = "TODO: UUID",
                        transform = transform_,
                        animation = animation_
                    }
                }) ;; ;

                if (result != null) Console.WriteLine("Updated node");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Update Node failed: {ex.Message}");
            }
        }

        public void DeleteNode(string id_)
        {
            try
            {
                DVRClientPacket<DVRDeleteNodeResult> result = this.SendAndReceiveData<DVRClientPacket<DVRDeleteNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/delete",
                    data = new DVRDeleteNodePacket() // Create Add Node Packet
                    {
                        id = id_,
                    }
                }); ; ;

                if (result != null) Console.WriteLine("Deleted node");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Delete Node failed: {ex.Message}");
            }
        }

        public void MoveNodeTo(string id_, string stop_, decimal[] position_, string rotate_, string interpolate_, bool followheight_, decimal speed_, decimal time_)
        {
            try
            {
                DVRClientPacket<DVRMoveNodeToResult> result = this.SendAndReceiveData<DVRClientPacket<DVRMoveNodeToResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/moveto",
                    data = new DVRMoveNodeToPacket() // Create Add Node Packet
                    {
                        id = id_,
                        stop = stop_,
                        position = position_,
                        rotate = rotate_,
                        interpolate = interpolate_,
                        followheight = followheight_,
                        speed = speed_,
                        time = time_
                    }
                }) ; ; ;

                if (result != null) Console.WriteLine("Moved node");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Move Node failed: {ex.Message}");
            }
        }

        public void AddTerrain(decimal[] size_, decimal[] heights_)
        {
            try {
                DVRClientPacket<DVRAddTerrainResult> result = this.SendAndReceiveData<DVRClientPacket<DVRAddTerrainResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/add",
                    data = new DVRAddTerrainPacket() // Create Add Node Packet
                    {
                        size = size_,
                        heights = heights_
                    }
                }) ; ;

                if (result != null) Console.WriteLine("Added Terrain");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add Terrain failed: {ex.Message}");

            }
        }

        public void UpdateTerrain()
        {
            try
            {
                DVRClientPacket<DVRUpdateTerrainResult> result = this.SendAndReceiveData<DVRClientPacket<DVRUpdateTerrainResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/update",
                    data = new DVRUpdateTerrainPacket() // Create Add Node Packet
                    {
                    }
                }); ;

                if (result != null) Console.WriteLine("Updated Terrain");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Update Terrain failed: {ex.Message}");

            }
        }

        public void DeleteTerrain()
        {
            try
            {
                DVRClientPacket<DVRDeleteTerrainResult> result = this.SendAndReceiveData<DVRClientPacket<DVRDeleteTerrainResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/delete",
                    data = new DVRDeleteTerrainPacket() // Create Add Node Packet
                    {
                    }
                }); ;

                if (result != null) Console.WriteLine("Deleted Terrain");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Delete Terrain failed: {ex.Message}");

            }
        }

        public void SetTimeSkyBox(decimal time_)
        {
            try
            {
                DVRClientPacket<DVRSetTimeSkyBoxResult> result = this.SendAndReceiveData<DVRClientPacket<DVRSetTimeSkyBoxResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/skybox/settime",
                    data = new DVRSetTimeSkyBoxPacket() // Create Add Node Packet
                    {
                        time = time_
                    }
                }); ; ;

                if (result != null) Console.WriteLine("Set time of SkyBox");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Set time of SkyBox failed: {ex.Message}");

            }
        }

        private void SendData(DVRClientPacket<DAbstract> clientPacket)
        {
            if (!this.client.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            client.SendData(new DVRTunnelPacket() { dest = this.tunnelId, data = clientPacket }); // create Tunnel Packet
        }

        private T SendAndReceiveData<T>(DVRClientPacket<DAbstract> sendPacket) where T : DAbstract
        {
            if(!this.client.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            T result = default(T);

            DVRTunnelReceivePacket<T> receivePacket = client.SendAndReceiveData<T>(new DVRTunnelPacket() { dest = this.tunnelId, data = sendPacket }); // create Tunnel Packet

            // unpack tunnel receive packet
            if (receivePacket.status != "error") result = receivePacket.data;
            else throw new VRClientException(receivePacket.msg);

            return result;
        }
    }
}

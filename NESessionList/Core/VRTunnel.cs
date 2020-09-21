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
                Console.WriteLine($"Add Node failed: {ex.Message}");

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

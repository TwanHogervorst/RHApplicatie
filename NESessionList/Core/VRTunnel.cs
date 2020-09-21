﻿using NESessionList.Data;
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

        public void AddNode()
        {
            try
            {
                DVRClientPacket<DVRAddNodeResult> result = this.SendAndReceiveData<DVRClientPacket<DVRAddNodeResult>>(new DVRClientPacket<DAbstract>()
                {
                    id = "scene/node/add",
                    data = new DVRAddNodePacket()
                    {
                        name = "Test"
                    }
                });

                if (result != null) Console.WriteLine($"Added node: {result.data.name} (uuid: {result.data.uuid})");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add Node failed: {ex.Message}");
            }
        }

        private void SendData(DVRClientPacket<DAbstract> clientPacket)
        {
            if (!this.client.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            client.SendData(new DVRTunnelPacket() { dest = this.tunnelId, data = clientPacket });
        }

        private T SendAndReceiveData<T>(DVRClientPacket<DAbstract> sendPacket) where T : DAbstract
        {
            if(!this.client.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            T result = default(T);

            DVRTunnelReceivePacket<T> receivePacket = client.SendAndReceiveData<T>(new DVRTunnelPacket() { dest = this.tunnelId, data = sendPacket });

            // unpack tunnel receive packet
            if (receivePacket.status != "error") result = receivePacket.data;
            else throw new VRClientException(receivePacket.msg);

            return result;
        }
    }
}

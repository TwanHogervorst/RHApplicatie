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

                if (result != null) Console.WriteLine($"Update node status: {result.data.status}");
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

                if (result != null) Console.WriteLine($"Delete node status: {result.data.status}");
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

                if (result != null) Console.WriteLine($"Move node status: {result.data.status}");
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

                if (result != null) Console.WriteLine($"Delete Terrain status: {result.data.status}");
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

        public void AddRoad(string route_, string diffuse_, string normal_, string specular_, decimal heightoffset_)
        {
            try
            {
                DVRClientPacket<DVRAddRoadResult> result = this.SendAndReceiveData<DVRClientPacket<DVRAddRoadResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/road/add",
                    data = new DVRAddRoadPacket() // Create Add Road Packet
                    {
                        route = route_,
                        diffuse = diffuse_,
                        normal = normal_,
                        specular = specular_,
                        heightoffset = heightoffset_
                    }
                }); ;

                if (result != null) Console.WriteLine($"Added Road uuid: {result.data.uuid}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add road failed: {ex.Message}");
            }
        }

        public void UpdateRoad(string id_, string route_, string diffuse_, string normal_, string specular_, decimal heightoffset_)
        {
            try
            {
                DVRClientPacket<DVRUpdateRoadResult> result = this.SendAndReceiveData<DVRClientPacket<DVRUpdateRoadResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/road/update",
                    data = new DVRUpdateRoadPacket() // Create Update Road Packet
                    {
                        id = id_,
                        route = route_,
                        diffuse = diffuse_,
                        normal = normal_,
                        specular = specular_,
                        heightoffset = heightoffset_
                    }
                }); ;

                if (result != null) Console.WriteLine($"Updated Road uuid: {result.data.uuid}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Update road failed: {ex.Message}");
            }
        }
        public void AddRoute()
        {
            //TODO
        }

        public void UpdateRoute()
        {
            //TODO
        }

        public void DeleteRoute(string id_)
        {
            try
            {
                DVRClientPacket<DVRRouteDeleteResult> result = this.SendAndReceiveData<DVRClientPacket<DVRRouteDeleteResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/delete",
                    data = new DVRRouteDeletePacket// Create Delete route packet
                    {
                        id = id_
                    }
                }); ;

                if (result != null) Console.WriteLine($"Status deleted route: {result.data.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Delete route failed: {ex.Message}");
            }
        }

        public void FollowRoute(string route_, string node_, decimal speed_, decimal offset_, string rotate_,
            decimal smoothing_, bool followHeight_, decimal[] rotateOffset_, decimal[] positionOffset_)
        {
            try
            {
                DVRClientPacket<DVRRouteFollowResult> result = this.SendAndReceiveData<DVRClientPacket<DVRRouteFollowResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/follow",
                    data = new DVRRouteFollowPacket() // Create route follow packet
                    {
                        route = route_,
                        node = node_,
                        speed = speed_,
                        offset = offset_,
                        rotate = rotate_,
                        smoothing = smoothing_,
                        followHeight = followHeight_,
                        rotateOffset = rotateOffset_,
                        positionOffset = positionOffset_
                    }
                }); ;

                if (result != null) Console.WriteLine($"Status follow route: {result.data.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Follow route failed: {ex.Message}");
            }
        }

        public void FollowRouteSpeed(string node_, decimal speed_)
        {
            try
            {
                DVRClientPacket<DVRRouteFollowSpeedResult> result = this.SendAndReceiveData<DVRClientPacket<DVRRouteFollowSpeedResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/follow/speed",
                    data = new DVRRouteFollowSpeedPacket() // Create Update Road Packet
                    {
                        node = node_,
                        speed = speed_
                    }
                }); ;

                if (result != null) Console.WriteLine($"Status route follow: {result.data.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"route follow speed failed: {ex.Message}");
            }
        }

        public void ShowRoute(bool show_)
        {
            try
            {
                DVRClientPacket<DVRRouteShowResult> result = this.SendAndReceiveData<DVRClientPacket<DVRRouteShowResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/show",
                    data = new DVRRouteShowPacket() // Create Update Road Packet
                    {
                        show = show_
                    }
                }); ;

                if (result != null) Console.WriteLine($"Status route show: {result.data.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"route show failed: {ex.Message}");
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

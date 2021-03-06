﻿using RHApplicatieLib.Abstract;
using RHApplicatieLib.Data;
using RHApplicatieLib.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RHApplicatieLib.Core
{
    public class VRTunnel
    {
        private string tunnelId;
        private VRClient client;

        public Dictionary<string, string> nodeList { get; private set; }
        public Dictionary<string, string> routeList { get; private set; }
        public Dictionary<string, string> roadList { get; private set; }

        public VRTunnel(string tunnelId, VRClient client)
        {
            this.tunnelId = tunnelId;
            this.client = client;
            this.nodeList = new Dictionary<string, string>();
            this.routeList = new Dictionary<string, string>();
            this.roadList = new Dictionary<string, string>();
        }

        public void SceneReset()
        {
            try
            {
                DVRClientReceivePacket<DVRSceneResetResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRSceneResetResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/reset"
                }); ;

                if (result?.status != "error") Console.WriteLine($"Scene Reset");
                else Console.WriteLine($"SceneReset Error: {result?.error ?? "NULL"}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Scene Reset failed: {ex.Message}");
            }
        }

        public void AddNode(string name_, DVRAddNodePacket.DComponents.DTransform transform_, DVRAddNodePacket.DComponents.DModel model_,
                            DVRAddNodePacket.DComponents.DTerrain terrain_, DVRAddNodePacket.DComponents.DPanel panel_, DVRAddNodePacket.DComponents.DWater water_, string parent_ = null)
        {
            try
            {
                DVRClientReceivePacket<DVRAddNodeResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRAddNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/add",
                    data = new DVRAddNodePacket() // Create Add Node Packet
                    {
                        name = name_,
                        parent = parent_,
                        components = new DVRAddNodePacket.DComponents
                        {
                            transform = transform_,
                            model = model_,
                            terrain = terrain_,
                            panel = panel_,
                            water = water_
                        }
                    }
                }); 

                if (result?.status != "error")
                {
                    Console.WriteLine($"Added node: {result.data.name} (uuid: {result.data.uuid})");
                    nodeList.Add(result.data.name, result.data.uuid);
                }
                else Console.WriteLine($"AddNode Error: {result?.error ?? "NULL"}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add Node failed: {ex.Message}");
            }
        }

        public void UpdateNode(string id_, DVRUpdateNodePacket.DTransform transform_ = null, DVRUpdateNodePacket.DAnimation animation_ = null, string parent_ = null)
        {
            try
            {
                DVRClientReceivePacket<DVRUpdateNodeResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRUpdateNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/update",
                    data = new DVRUpdateNodePacket() // Create Update Node Packet
                    {
                        id = id_,
                        parent = parent_,
                        transform = transform_,
                        animation = animation_
                    }
                });

                if (result?.status != "error") Console.WriteLine($"Update node status: {result.status}");
                else Console.WriteLine($"UpdateNode Error: {result?.error ?? "NULL"}");
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
                DVRClientReceivePacket<DVRDeleteNodeResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRDeleteNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/delete",
                    data = new DVRDeleteNodePacket() // Create Delete Node Packet
                    {
                        id = id_,
                    }
                }); ; ;

                if (result?.status != "error")
                {
                    Console.WriteLine($"Delete node status: {result.status}");
                    nodeList.Remove(nodeList.FirstOrDefault(e => e.Value == id_).Key);
                }
                else Console.WriteLine($"DeleteNode Error: {result?.error ?? "NULL"}");
                if (result != null)
                {
                    //Console.WriteLine($"Delete node status: {result.data.status}");

                }
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Delete Node failed: {ex.Message}");
            }
        }

        public void FindNode(string name_)
        {
            try
            {
                DVRClientPacketArrayResponse<DVRFindNodeResult> result = this.SendAndReceiveData<DVRClientPacketArrayResponse<DVRFindNodeResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/find",
                    data = new DVRFindNodePacket // Create Find Node Packet
                    {
                        name = name_
                    }
                }); ; ;

                if (result?.status != "error")
                {
                    Console.WriteLine($"Node info: {result.data[0].uuid}");
                    nodeList.Add(name_, result.data[0].uuid);
                }
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Delete Node failed: {ex.Message}");
            }
        }

        public void MoveNodeTo(string id_, decimal[] position_, string rotate_, string interpolate_, bool followheight_, decimal speed_ = 0, decimal time_ = 0, string stop_ = null)
        {
            try
            {
                DVRClientReceivePacket<DVRMoveNodeToResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRMoveNodeToResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/node/moveto",
                    data = new DVRMoveNodeToPacket() // Create Move Node To Packet
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
                }); ; ;

                if (result?.status != "error") Console.WriteLine($"Move node status: {result.status}");
                else Console.WriteLine($"MoveNode Error: {result?.error ?? "NULL"}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Move Node failed: {ex.Message}");
            }
        }

        public void AddNodeLayer(string id_, string diffuse_, string normal_, decimal minHeight_,
            decimal maxHeight_, decimal fadeDist_)
        {
            try
            {
                DVRClientReceivePacket<DVRNodeAddlayerResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRNodeAddlayerResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/add",
                    data = new DVRNodeAddlayerPacket() // Create Add Node layer packet
                    {
                        id = id_,
                        diffuse = diffuse_,
                        normal = normal_,
                        minHeight = minHeight_,
                        maxHeight = maxHeight_,
                        fadeDist = fadeDist_
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine("Added Terrain");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add Terrain failed: {ex.Message}");

            }
        }

        public void AddTerrain(decimal[] size_, decimal[] heights_)
        {
            try
            {
                DVRClientReceivePacket<DVRAddTerrainResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRAddTerrainResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/add",
                    data = new DVRAddTerrainPacket() // Create Add Terrain Packet
                    {
                        size = size_,
                        heights = heights_
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine("Added Terrain");
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
                DVRClientReceivePacket<DVRUpdateTerrainResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRUpdateTerrainResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/update",
                    data = new DVRUpdateTerrainPacket() // Create Update Terrain Packet
                    {
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine("Updated Terrain");
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
                DVRClientReceivePacket<DVRDeleteTerrainResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRDeleteTerrainResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/terrain/delete",
                    data = new DVRDeleteTerrainPacket() // Create Delete Terrain Packet
                    {
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"Delete Terrain status: {result.data?.status}");
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
                DVRClientReceivePacket<DVRSetTimeSkyBoxResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRSetTimeSkyBoxResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/skybox/settime",
                    data = new DVRSetTimeSkyBoxPacket() // Create Set Time SkyBox Packet
                    {
                        time = time_
                    }
                }); ; ;

                if (result?.status != "error") Console.WriteLine("Set time of SkyBox");
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
                DVRClientReceivePacket<DVRAddRoadResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRAddRoadResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
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

                if (result?.status != "error")
                {
                    Console.WriteLine($"Added Road uuid: {result.data.uuid}");
                    roadList.Add(route_, result.data.uuid);
                }
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
                DVRClientReceivePacket<DVRUpdateRoadResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRUpdateRoadResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
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

                if (result?.status != "error") Console.WriteLine($"Updated Road uuid: {result.data.uuid}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Update road failed: {ex.Message}");
            }
        }
        public void AddRoute(string name, List<DVRAddRouteNodesPacket> nodes_)
        {
            try
            {
                DVRClientReceivePacket<DVRAddRouteResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRAddRouteResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/add",
                    data = new DVRAddRoutePacket() // Create Add Route Packet
                    {
                        nodes = nodes_
                    }
                }); ;

                if (result?.status != "error")
                {
                    Console.WriteLine($"Add Route : {result.data.uuid}");
                    routeList.Add(name, result.data.uuid);
                }
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Add route failed: {ex.Message}");
            }
        }

        public void UpdateRoute(string id_, List<DVRUpdateRouteNodesPacket> nodes_)
        {
            try
            {
                DVRClientReceivePacket<DVRRouteUpdateResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRRouteUpdateResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/add",
                    data = new DVRRouteUpdatePacket() // Create Update Route Packet
                    {
                        id = id_,
                        nodes = nodes_
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"Update Route : {result.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Update route failed: {ex.Message}");
            }
        }

        public void DeleteRoute(string id_)
        {
            try
            {
                DVRClientReceivePacket<DVRRouteDeleteResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRRouteDeleteResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/delete",
                    data = new DVRRouteDeletePacket// Create Delete route packet
                    {
                        id = id_
                    }
                }); ;

                if (result?.status != "error")
                {
                    Console.WriteLine($"Status deleted route: {result.status}");
                    routeList.Remove(routeList.FirstOrDefault(e => e.Value == id_).Key);
                    roadList.Remove(id_);
                }
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
                DVRClientReceivePacket<DVRRouteFollowResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRRouteFollowResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
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

                if (result?.status != "error") Console.WriteLine($"Status follow route: {result.status}");
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
                DVRClientReceivePacket<DVRRouteFollowSpeedResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRRouteFollowSpeedResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/follow/speed",
                    data = new DVRRouteFollowSpeedPacket() // Create Update Road Packet
                    {
                        node = node_,
                        speed = speed_
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"Status route follow: {result.status}");
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
                DVRClientReceivePacket<DVRRouteShowResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRRouteShowResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "route/show",
                    data = new DVRRouteShowPacket() // Create Update Road Packet
                    {
                        show = show_
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"Status route show: {result.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"route show failed: {ex.Message}");
            }
        }

        public void ClearPanel(string panelId)
        {
            try
            {
                DVRClientReceivePacket<DVRClearPanelResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRClearPanelResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/panel/clear",
                    data = new DVRClearPanelPacket() // Create Update Route Packet
                    {
                        id = panelId
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"Clear Panel : {result.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Clear Panel failed: {ex.Message}");
            }
        }

        public void SwapPanel(string panelId)
        {
            try
            {
                DVRClientReceivePacket<DVRSwapPanelResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRSwapPanelResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/panel/swap",
                    data = new DVRSwapPanelPacket() // Create Update Route Packet
                    {
                        id = panelId
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"Swap Panel : {result.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"Swap panel failed: {ex.Message}");
            }
        }

        public void DrawTextPanel(string panelId, string text_, decimal[] position_, decimal size_ = 1, decimal[] color_ = null, string font_ = "segoeui")
        {

            try
            {
                DVRClientReceivePacket<DVRDrawTextPanelResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRDrawTextPanelResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/panel/drawtext",
                    data = new DVRDrawTextPanelPacket() // Create Update Route Packet
                    {
                        id = panelId,
                        text = text_,
                        position = position_,
                        size = size_,
                        color = color_,
                        font = font_
                    }
                }); ;

                if (result?.status != "error") Console.WriteLine($"DrawText Panel : {result.status}");
            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"DrawText panel failed: {ex.Message}");
            }
        }

        public void GetScene()
        {
            try
            {
                DVRClientReceivePacket<DVRGetSceneResult> result = this.SendAndReceiveData<DVRClientReceivePacket<DVRGetSceneResult>>(new DVRClientPacket<DAbstract>() // create VRClient Packet
                {
                    id = "scene/get",
                    data = new DVRRouteShowPacket() // Create get scene Packet
                    {
                    }
                }); ;

                if (result?.status != "error")
                {
                    Console.WriteLine($"Scene info: {result.data.sceneData.ToString()}");

                    //foreach (node in result)
                    //{
                    //    if (!nodeList.ContainsKey(name))
                    //    {
                    //        nodeList.Add(name, uuid);
                    //    }
                    //}
                }


            }
            catch (VRClientException ex)
            {
                Console.WriteLine($"get scene failed: {ex.Message}");
            }
        }

        private void SendData(DVRClientPacket<DAbstract> clientPacket)
        {
            if (!this.client.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            client.SendData(new DVRTunnelPacket() { dest = this.tunnelId, data = clientPacket }); // create Tunnel Packet
        }

        private T SendAndReceiveData<T>(DVRClientPacket<DAbstract> sendPacket) where T : DAbstract
        {
            if (!this.client.IsConnected) throw new InvalidOperationException("Not connected! Please connect to the server first");

            T result = default(T);

            DVRTunnelReceivePacket<T> receivePacket = client.SendAndReceiveData<T>(new DVRTunnelPacket() { dest = this.tunnelId, data = sendPacket }); // create Tunnel Packet

            // unpack tunnel receive packet
            if (receivePacket.status != "error") result = receivePacket.data;
            else throw new VRClientException(receivePacket.msg);

            if (result is DVRClientReceivePacket)
            {
                if ((result as DVRClientReceivePacket)?.id == "callback")
                {
                    throw new VRCallbackException("Invalid package");
                }
            }
            //else if (result is DVRClientPacketArrayResponse<>)
            //{
            //    if ((result as DVRClientPacketArrayResponse<>)?.id == "callback")
            //    {
            //        throw new VRCallbackException("Invalid package");
            //    }
            //}

            return result;
        }

        public void Disconnect()
        {
            //TODO disconnect the tunnel VREngine
            this.client.Disconnect();
        }
    }

}

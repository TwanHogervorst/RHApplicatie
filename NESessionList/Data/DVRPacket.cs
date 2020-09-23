using Newtonsoft.Json;
using RHApplicationLib.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace NESessionList.Data
{
    public class DVRClientPacket<T> : DAbstract where T : DAbstract
    {
        public string id;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public T data;
    }

    public class DVRClientPacketArrayResponse<T> : DAbstract where T : DAbstract
    {
        public string id;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<T> data;
    }

    public class DVRClientReceivePacket : DAbstract
    {
        public string id;
        public string status;
        public string msg;
    }

    public class DVRTunnelReceivePacket<T> : DVRClientReceivePacket
    {
        public T data;
    }

    public class DVRTunnelPacket : DAbstract
    {
        public string dest;
        public DAbstract data;
    }

    public class DVRSessionItem : DAbstract
    {
        public string id;
        public DateTime beginTime;
        public DateTime lastPing;
        public List<DFps> fps;
        public List<string> features;
        public DClientInfo clientinfo;

        public class DFps
        {
            public decimal time;
            public decimal fps;
        }

        public class DClientInfo
        {
            public string host;
            public string user;
            public string file;
            public string renderer;
        }
    }

    public class DVRTunnelCreate : DAbstract
    {
        public string session;
        public string key;
    }

    public class DVRAddNodePacket : DAbstract
    {
        public string name;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string parent;
        public DComponents components;

        public class DComponents : DAbstract
        {
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public DTransform transform;
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public DModel model;
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public DTerrain terrain;
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public DPanel panel;
            [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
            public DWater water;

            public class DTransform : DAbstract
            {
                public decimal[] position;
                public decimal scale;
                public decimal[] rotation;

                public DTransform(decimal[] position, decimal scale, decimal[] rotation)
                {
                    this.position = position;
                    this.scale = scale;
                    this.rotation = rotation;
                }
            }

            public class DModel : DAbstract
            {
                public string file;
                public bool cullbackfaces = false;
                public bool animated = false;
                public string animation;

                public DModel(string file, bool cullbackfaces, bool animated, string animation)
                {
                    this.file = file;
                    this.cullbackfaces = cullbackfaces;
                    this.animated = animated;
                    this.animation = animation;
                }
            }

            public class DTerrain : DAbstract
            {
                public bool smoothnormals = true;

                public DTerrain(bool smoothnormals)
                {
                    this.smoothnormals = smoothnormals;
                }
            }

            public class DPanel : DAbstract
            {
                public decimal[] size;
                public decimal[] resolution;
                public decimal[] background;
                public bool castShadow = true;

                public DPanel(decimal[] size, decimal[] resolution, decimal[] background, bool castShadow)
                {
                    this.size = size;
                    this.resolution = resolution;
                    this.background = background;
                    this.castShadow = castShadow;
                }
            }

            public class DWater : DAbstract
            {
                public decimal[] size;
                public decimal resolution;

                public DWater(decimal[] size, decimal resolution)
                {
                    this.size = size;
                    this.resolution = resolution;
                }
            }
        }
    }

    public class DVRUpdateNodePacket : DAbstract
    {
        public string id;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string parent;
        public DTransform transform;
        public DAnimation animation;

        public class DTransform : DAbstract
        {
            public decimal[] position;
            public decimal scale;
            public decimal[] rotation;

            public DTransform(decimal[] position, decimal scale, decimal[] rotation)
            {
                this.position = position;
                this.scale = scale;
                this.rotation = rotation;
            }
        }
        public class DAnimation : DAbstract
        {
            public string name;
            public decimal speed;

            public DAnimation(string name_, decimal speed_)
            {
                this.name = name_;
                this.speed = speed_;
            }
        }

    }

    public class DVRDeleteNodePacket : DAbstract
    {
        public string id;
    }

    public class DVRMoveNodeToPacket : DAbstract
    {
        public string id;
        public string stop;
        public decimal[] position;
        public string rotate;
        public string interpolate;
        public bool followheight;
        public decimal speed;
        public decimal time;
    }

    public class DVRAddTerrainPacket : DAbstract
    {
        public decimal[] size;
        public decimal[] heights;
    }

    public class DVRUpdateTerrainPacket : DAbstract
    {

    }

    public class DVRDeleteTerrainPacket : DAbstract
    {

    }

    public class DVRSetTimeSkyBoxPacket : DAbstract
    {
        public decimal time;
    }

    public class DVRAddNodeResult : DAbstract
    {
        public string uuid;
        public string name;
    }

    public class DVRUpdateNodeResult : DAbstract
    {
        public string status;
    }

    public class DVRDeleteNodeResult : DAbstract
    {
        public string status;
    }

    public class DVRMoveNodeToResult : DAbstract
    {
        public string status;
    }

    public class DVRAddTerrainResult : DAbstract
    {
    }

    public class DVRUpdateTerrainResult : DAbstract
    {
    } 

    public class DVRDeleteTerrainResult : DAbstract
    {
        public string status;
    }

    public class DVRSetTimeSkyBoxResult : DAbstract
    {
    }

    public class DVRAddRoadPacket : DAbstract
    {
        public string route;
        public string diffuse;
        public string normal;
        public string specular;
        public decimal heightoffset;
    }

    public class DVRAddRoadResult : DAbstract
    {
        public string uuid;
    }

    public class DVRUpdateRoadPacket : DAbstract
    {
        public string id;
        public string route;
        public string diffuse;
        public string normal;
        public string specular;
        public decimal heightoffset;
    }

    public class DVRUpdateRoadResult : DAbstract
    {
        public string uuid;
    }

    public class DVRAddRoutePacket : DAbstract
    {
        public List<DVRAddRouteNodesPacket> nodes;
    }

    public class DVRAddRouteNodesPacket
    {
        public decimal[] pos;
        public decimal[] dir;
    }

    public class DVRAddRouteResult : DAbstract
    {
        public string status;
        public string uuid;
    }

    public class DVRRouteUpdatePacket : DAbstract
    {
        public string id;
        public List<DVRUpdateRouteNodesPacket> nodes;
    }

    public class DVRUpdateRouteNodesPacket
    {
        public int index;
        public decimal[] pos;
        public decimal[] dir;
    }

    public class DVRRouteUpdateResult : DAbstract
    {
        public string status;
    }

    public class DVRRouteDeletePacket : DAbstract
    {
        public string id;
    }

    public class DVRRouteDeleteResult : DAbstract
    {
        public string status;
    }

    public class DVRRouteFollowPacket : DAbstract
    {
        public string route;
        public string node;
        public decimal speed;
        public decimal offset;
        public string rotate;
        public decimal smoothing;
        public bool followHeight;
        public decimal[] rotateOffset;
        public decimal[] positionOffset;
    }

    public class DVRRouteFollowResult : DAbstract
    {
        public string status;
    }

    public class DVRRouteFollowSpeedPacket : DAbstract
    {
        public string node;
        public decimal speed;
    }

    public class DVRRouteFollowSpeedResult : DAbstract
    {
        public string status;
    }

    public class DVRRouteShowPacket : DAbstract
    {
        public bool show;
    }

    public class DVRRouteShowResult : DAbstract
    {
        public string status;
    }
}

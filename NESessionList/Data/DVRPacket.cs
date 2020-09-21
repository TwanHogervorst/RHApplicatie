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
            }

            public class DModel : DAbstract
            {
                public string file;
                public bool cullbackfaces = false;
                public bool animated = false;
                public string animation;
            }

            public class DTerrain : DAbstract
            {
                public bool smoothnormals = true;
            }

            public class DPanel : DAbstract
            {
                public decimal[] size;
                public decimal[] resolution;
                public decimal[] background;
                public bool castShadow = true;
            }

            public class DWater : DAbstract
            {
                public decimal[] size;
                public decimal resolution;
            }
        }
    }

    public class DVRAddNodeResult : DAbstract
    {
        public string uuid;
        public string name;
    }
}

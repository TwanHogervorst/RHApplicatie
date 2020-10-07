using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RHApplicationLib.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerUtils
{
    class DataPacket<T> : DAbstract where T : DAbstract
    {
        public string sender;
        public string type; // Id can for example be "chatMessage" or "LoginStatus"
        public T data; // Content of the message
    }

    class DataPacket : DAbstract
    {
        public string sender;
        public string type;
        [JsonProperty]
        private JObject data;

        public DataPacket<T> GetData<T>() where T : DAbstract
        {
            return new DataPacket<T> {
                sender = this.sender,
                type = this.type, 
                data = this.data.Value<T>() 
            };
        }
    }

    class LoginPacket : DAbstract
    {
        public string username;
        public string password;
    }

    class LoginResponse : DAbstract
    {
        public string status;
    }

    class ChatPacket : DAbstract
    {
        public string chatMessage;
    }

    class BikeDataPacket : DAbstract
    {
        //TODO bikeData
        public decimal speed;
        public decimal heartbeat;
        public decimal resistance;
        public decimal distanceTraveled;
        public decimal power;
    }
}

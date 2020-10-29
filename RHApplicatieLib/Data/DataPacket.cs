using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RHApplicatieLib.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace RHApplicatieLib.Data
{
    public class DataPacket<T> : DAbstract where T : DAbstract
    {
        public string sender;
        public string type; // Id can for example be "chatMessage" or "LoginStatus"
        public T data; // Content of the message
    }

    public class DataPacket : DAbstract
    {
        public string sender;
        public string type;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        private JObject data;

        public DataPacket<T> GetData<T>() where T : DAbstract
        {
            return new DataPacket<T> {
                sender = this.sender,
                type = this.type, 
                data = this.data.ToObject<T>() 
            };
        }
    }

    public class LoginPacket : DAbstract
    {
        public bool isClient;
        public string username;
        public string password;
    }

    public class LoginResponse : DAbstract
    {
        public bool isClient;
        public string status;
    }

    public class ChatPacket : DAbstract
    {
        public string receiver;
        public string chatMessage;
        public bool isDoctorMessage;
    }

    public class ClientListPacket : DAbstract
    {
        public Dictionary<string, bool> clientList;
    }

    public class UserNamePacket : DAbstract
    {
        public string clientUserName;
    }

    public class UserNamePacketResponse : DAbstract
    {
        public string doctorUserName;
    }

    public class ResistancePacket : DAbstract
    {
        public string receiver;
        public int resistance;
    }

    public class RequestClientDataPacket : DAbstract
    {

    }

    public class StartStopPacket : DAbstract
    {
        public string receiver;
        public string doctor;
        public bool startSession;
    }

    public class RequestSessionStatePacket : DAbstract
    {
        public string receiver;
    }

    public class ResponseSessionStatePacket : DAbstract
    {
        public string receiver;
        public string sessionId;
        public bool sessionState;
        public DateTime startTimeSession;
    }

    //Eventueel nog responsePackets

    public class BikeDataPacket : DAbstract
    {
        public string doctor;
        public double speed;
        public int heartbeat;
        public double elapsedTime;
        public double distanceTraveled;
        public int power;
        public int resistance;
        public DateTime timestamp;
    }

    public class EmergencyStopPacket : DAbstract
    {
        public string receiver;
        public bool startSession;
        public int resistance;
    }

    public class EmergencyResponsePacket : DAbstract
    {
        public string receiver;
        public bool state;
    }

    public class RequestTrainingList : DAbstract
    {
        public string forClient;
    }

    public class ResponseTrainingList : DAbstract
    {
        public string forClient;
        public List<string> trainingList = new List<string>();
    }

    public class RequestTrainingData : DAbstract
    {
        public string forClient;
        public string trainingName;
    }

    public class ResponseTrainingData : DAbstract
    {
        public string forClient;
        public string trainingName;
        public List<BikeDataPacket> trainingData = new List<BikeDataPacket>();
    }

    public class DisconnectRequestPacket : DAbstract
    {
    }

    public class RequestBikeStatePacket : DAbstract
    {
        public string forClient;
    }

    public class ResponseBikeState : DAbstract
    {
        public bool bikeIsConnected;
    }
}

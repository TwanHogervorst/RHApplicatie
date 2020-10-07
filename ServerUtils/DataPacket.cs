using RHApplicationLib.Abstract;

namespace ServerUtils
{
    class DataPacket<T> : DAbstract where T : DAbstract
    {
        public string type; // Id can for example be "chatMessage" or "LoginStatus"
        public T data; // Content of the message
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
    }
}

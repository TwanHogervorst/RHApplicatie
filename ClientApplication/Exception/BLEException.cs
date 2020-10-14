namespace ClientApplication.Exception
{
    public class BLEException : System.Exception
    {
        public int ErrorCode { get; private set; }

        public BLEException(int errorCode)
            : base("BLE Error: " + errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public BLEException(int errorCode, string message)
            : base(message)
        {
            this.ErrorCode = errorCode;
        }

    }
}

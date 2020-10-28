namespace ClientApplication.Exception
{
    class VRClientException : System.Exception
    {
        public VRClientException(string message) : base(message)
        {
        }


    }

    class VRCallbackException : System.Exception
    {
        public VRCallbackException(string message) : base(message)
        {

        }
    }
}

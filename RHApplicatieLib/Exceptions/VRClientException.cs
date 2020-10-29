namespace RHApplicatieLib.Exceptions
{
    public class VRClientException : System.Exception
    {
        public VRClientException(string message) : base(message)
        {
        }


    }

    public class VRCallbackException : System.Exception
    {
        public VRCallbackException(string message) : base(message)
        {

        }
    }
}

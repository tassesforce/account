namespace Exceptions
{
    internal class UnknownException : System.Exception
    {
        public UnknownException() {}
        public UnknownException(string message) : base(message) {}
        public UnknownException(string message, System.Exception innerException) : base(message, innerException) {}
    }
}
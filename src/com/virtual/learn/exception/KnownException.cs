namespace Exceptions
{
    internal class KnownException : System.Exception
    {
        public KnownException() {}
        public KnownException(string message) : base(message) {}
        public KnownException(string message, System.Exception innerException) : base(message, innerException) {}
    }
}
using Exceptions;

namespace compte.Exceptions
{
    internal class KnownAccountException : KnownException
    {
        public KnownAccountException() {}
        public KnownAccountException(string message) : base(message) {}
        public KnownAccountException(string message, System.Exception innerException) : base(message, innerException) {}
    }
}
using Exceptions;

namespace mission.Exceptions
{
    internal class UnknownMissionException : UnknownException
    {
        public UnknownMissionException() {}
        public UnknownMissionException(string message) : base(message) {}
        public UnknownMissionException(string message, System.Exception innerException) : base(message, innerException) {}
    }
}
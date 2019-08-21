using Exceptions;

namespace mission.Exceptions
{
    internal class KnownMissionException : KnownException
    {
        public KnownMissionException() {}
        public KnownMissionException(string message) : base(message) {}
        public KnownMissionException(string message, System.Exception innerException) : base(message, innerException) {}
    }
}
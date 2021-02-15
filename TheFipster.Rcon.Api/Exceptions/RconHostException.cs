using System;

namespace TheFipster.Rcon.Api.Exceptions
{
    public class RconHostException : Exception
    {
        public RconHostException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

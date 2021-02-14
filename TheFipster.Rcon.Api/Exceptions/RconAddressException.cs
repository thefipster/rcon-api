using System;

namespace TheFipster.Rcon.Api.Exceptions
{
    public class RconAddressException : Exception
    {
        public RconAddressException()
        {
        }

        public RconAddressException(string message) : base(message)
        {
        }
    }
}

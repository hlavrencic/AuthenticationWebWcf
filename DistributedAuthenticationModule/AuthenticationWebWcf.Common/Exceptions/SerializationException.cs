using System;

namespace AuthenticationWebWcf.Common.Exceptions
{
    public class SerializationException : AuthenticationException
    {
        public SerializationException(string msg)
            : base(msg)
        {
        }

        public SerializationException(string msg, Exception ex)
            : base(msg, ex)
        {
        }
    }
}

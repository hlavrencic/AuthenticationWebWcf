using System;

namespace AuthenticationWebWcf.Common.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
        {
        }

        public AuthenticationException(string msg) : base(msg)
        {
        }

        public AuthenticationException(string msg, Exception ex)
            : base(msg, ex)
        {
        }
    }
}

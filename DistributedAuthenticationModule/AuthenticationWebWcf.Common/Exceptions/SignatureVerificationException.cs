using System;

namespace AuthenticationWebWcf.Common.Exceptions
{
    public class SignatureVerificationException : AuthenticationException
    {
        public SignatureVerificationException(string message)
            : base(message)
        {
        }

        public SignatureVerificationException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}
using System;

namespace AuthenticationWebWcf.Common.Exceptions
{
    public class ExpiredException : AuthenticationException
    {
        public DateTime Expired { get; private set; }

        public ExpiredException(DateTime expired)
        {
            Expired = expired;
        }
    }
}

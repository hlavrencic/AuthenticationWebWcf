using System;
using AuthenticationWebWcf.Common.Interfaces;

namespace AuthenticationWebWcf.Common
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.Now;
        }
    }
}

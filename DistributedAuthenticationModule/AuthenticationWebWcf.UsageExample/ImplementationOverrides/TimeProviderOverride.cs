using System;
using AuthenticationWebWcf.Common.Interfaces;

namespace AuthenticationWebWcf.UsageExample.ImplementationOverrides
{
    public class TimeProviderOverride : ITimeProvider
    {
        #region Implementation of ITimeProvider

        public DateTime GetDateTime()
        {
            return new DateTime(2016,5,1);
        }

        #endregion
    }
}

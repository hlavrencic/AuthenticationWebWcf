using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.UsageExample.WcfHost.WebServiceExample
{
    public class SecuredData : AuthenticatedDto
    {
        public string Client { get; set; }
    }
}
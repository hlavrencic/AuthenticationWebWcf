using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    public class SecuredData : AuthenticatedDto
    {
        public string Client { get; set; }
    }
}
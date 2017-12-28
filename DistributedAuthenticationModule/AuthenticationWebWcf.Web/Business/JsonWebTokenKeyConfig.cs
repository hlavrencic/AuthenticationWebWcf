using System.Web.Configuration;

namespace AuthenticationWebWcf.Web.Business
{
    public class JsonWebTokenKeyConfig : IJsonWebTokenKeyConfig
    {
        private string tokenKey;

        public string GetTokenKey()
        {
            return tokenKey ?? (tokenKey = WebConfigurationManager.AppSettings["TokenKey"]);
        }
    }
}

using System.Collections.Generic;
using System.Web;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Service.Helpers;

namespace AuthenticationWebWcf.Web.Business
{
    public class AuthenticatedUserManager<T> : IAuthenticatedUserManager<T> where T : AuthenticatedDto
    {
        private readonly IAuthenticatedUserReader<T> authenticatedUserReader;
        private readonly ITokenCookieManager tokenCookieManager;

        public AuthenticatedUserManager(IAuthenticatedUserReader<T> authenticatedUserReader, ITokenCookieManager tokenCookieManager)
        {
            this.authenticatedUserReader = authenticatedUserReader;
            this.tokenCookieManager = tokenCookieManager;
        }

        public KeyValuePair<string, T> ReadUser(HttpRequestBase request)
        {
            var token = tokenCookieManager.Read(request.Cookies);

            if (string.IsNullOrEmpty(token))
            {
                return new KeyValuePair<string, T>();
            }

            HttpContextUserAuthentication.Token = token;

            return authenticatedUserReader.GetUser(token);
        }
    }
}

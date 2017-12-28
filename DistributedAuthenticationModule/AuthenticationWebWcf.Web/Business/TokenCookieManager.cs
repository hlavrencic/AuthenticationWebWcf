using System.Web;
using System.Web.Security;

namespace AuthenticationWebWcf.Web.Business
{
    public class TokenCookieManager : ITokenCookieManager
    {
        public void Write(string token, HttpCookieCollection cookieCollection)
        {
            var authCookie = new HttpCookie(TokenWebSecurityConfigurations.CookieName, token);
            cookieCollection.Remove(FormsAuthentication.FormsCookieName);
            cookieCollection.Add(authCookie);
        }

        public string Read(HttpCookieCollection cookieCollection)
        {
            var cookie = cookieCollection[TokenWebSecurityConfigurations.CookieName];
            return cookie != null ? cookie.Value : null;
        }

        public void SetCookie(HttpResponseBase response, string token)
        {
            Write(token, response.Cookies);
        }
    }
}

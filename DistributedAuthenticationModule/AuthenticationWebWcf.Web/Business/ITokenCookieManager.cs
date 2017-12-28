using System.Web;

namespace AuthenticationWebWcf.Web.Business
{
    public interface ITokenCookieManager
    {
        string Read(HttpCookieCollection cookieCollection);

        void Write(string token, HttpCookieCollection cookieCollection);

        void SetCookie(HttpResponseBase response, string token);
    }
}
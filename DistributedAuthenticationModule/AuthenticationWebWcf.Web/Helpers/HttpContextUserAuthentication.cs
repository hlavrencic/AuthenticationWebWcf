using System.Web;

namespace AuthenticationWebWcf.Web.Helpers
{
    public static class HttpContextUserAuthentication
    {
        public static string Token
        {
            get { return HttpContext.Current.Items["Token"] as string; }
            set { HttpContext.Current.Items["Token"] = value; }
        }
    }
}

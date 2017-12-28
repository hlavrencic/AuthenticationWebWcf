namespace AuthenticationWebWcf.Web.Business
{
    public static class TokenWebSecurityConfigurations
    {
        private static string cookieName = ".TokenWebCookieJWT";

        public static string CookieName
        {
            get { return cookieName; }
        }
    }
}

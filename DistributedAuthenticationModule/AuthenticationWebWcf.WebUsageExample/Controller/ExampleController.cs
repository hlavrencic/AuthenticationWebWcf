using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using AuthenticationWebWcf.UsageExample.WebServiceExample;
using AuthenticationWebWcf.Web.Business;
using AuthenticationWebWcf.WebUsageExample.External;
using AuthenticationWebWcf.WebUsageExample.Model;

namespace AuthenticationWebWcf.WebUsageExample.Controller
{
    public class ExampleController : ApiController
    {
        private readonly ILog logger;
        private readonly IService2 service2;
        private readonly IAuthenticationService authenticationService;
        private readonly ITokenCookieManager tokenCookieManager;

        public ExampleController(
            ILog logger,
            IService2 service2,
            IAuthenticationService authenticationService,
            ITokenCookieManager tokenCookieManager)
        {
            this.logger = logger;
            this.service2 = service2;
            this.authenticationService = authenticationService;
            this.tokenCookieManager = tokenCookieManager;
        }

        //public void Log(UserLoginModel loginModel)
        //{
        //    var token = authenticationService.Auth(loginModel);
        //    tokenCookieManager.SetCookie(, token);

        //}

        //public HttpResponseMessage Get()
        //{
        //    var resp = new HttpResponseMessage();

        //    var cookie = new CookieHeaderValue("session-id", "12345");
        //    cookie.Expires = DateTimeOffset.Now.AddDays(1);
        //    cookie.Domain = Request.RequestUri.Host;
        //    cookie.Path = "/";

        //    resp.Headers.AddCookies(new CookieHeaderValue[] { cookie });
        //    return resp;
        //}

        [CustomExceptionFilter]
        public void GetRoles()
        {
            service2.CallWebService();
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.SelfHost;
using AuthenticationWebWcf.Common;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Providers;
using AuthenticationWebWcf.UsageExample.WebServiceExample;
using AuthenticationWebWcf.Web.Business;
using AuthenticationWebWcf.WebUsageExample.External;
using AuthenticationWebWcf.WebUsageExample.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using Ninject;
using Ninject.Web.Common.SelfHost;
using TestingTools.WcfSelfHost;

namespace AuthenticationWebWcf.WebUsageExample
{
    [TestClass]
    public class WebTest
    {
        private SelfHostWcf host;

        private WcfClient client;

        private IService2 service2;
        private Mock<ILog> logMock;
        private NinjectSelfHostBootstrapper serverApi;
        private IAutenticationConverterWithAppKey authConverterWithAppKey;


        [TestInitialize]
        public void Init()
        {
            logMock = new Mock<ILog>();

            client = new WcfClient();

            var provider = new ServiceProvider(logMock.Object, new WcfClient());
            host = new SelfHostWcf(provider);
            host.Init<IService1>();

            host = new SelfHostWcf(provider);
            host.Init<IService2>();


            service2 = client.CreateClient<IService2>("IService2WebTokenEndpointBehaviorExtension");

            var config = new HttpSelfHostConfiguration("http://localhost:81");
            
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate:"{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
            serverApi = new NinjectSelfHostBootstrapper(CreateKernelCallback, config);
            serverApi.Start();

            var kernel = CreateKernelCallback();
            authConverterWithAppKey = kernel.Get<IAutenticationConverterWithAppKey>();
        }

        private IKernel CreateKernelCallback()
        {
            var kernel = new StandardKernel();
            kernel.Bind<ILog>().ToConstant(logMock.Object);
            kernel.Bind<IService2>().ToConstant(service2);
            kernel.Bind<IProvider>().ToMethod(c => ServiceProviderInitializer.Intialize());
            kernel.Bind<IAutenticationConverterWithAppKey>().ToMethod(c => c.Kernel.Get<IProvider>().Get<AutenticationConverterPropKey>()).OnActivation(c => c.Key = "1234");
            kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
            return kernel;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            host.Dispose();
            client.Dispose();
            serverApi.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var result = GetCookies("http://localhost:81/Example/Log", new UserLoginModel() { User = "user1", Password = "pass1" });
            Assert.AreEqual("", result);
        }


        [TestMethod]
        public void TestMethod2()
        {
            SecuredData securedDataParsed = null;
            logMock.Setup(s => s.Log(It.IsAny<SecuredData>())).Callback<SecuredData>(c => securedDataParsed = c);

            var guid = new Guid();
            var securedDataSent = new SecuredData()
            {
                Client = "Client1",
                FechaExpiracion = new DateTime(2017, 1, 1),
                Guid = guid.ToString(),
                Nombre = "Name1",
                Permisos = new[] { "Role1", "Role2" }
            };
            var token = authConverterWithAppKey.Encrypt(securedDataSent);

            var result = GetResponse("http://localhost:81/Example/GetRoles", securedDataSent, token);

            Assert.AreEqual("00000000-0000-0000-0000-000000000000", securedDataParsed.Guid);
            Assert.AreEqual("Client1", securedDataParsed.Client);
            Assert.AreEqual(new DateTime(2017, 1, 1), securedDataParsed.FechaExpiracion);
            Assert.AreEqual("Name1", securedDataParsed.Nombre);
            Assert.AreEqual(2, securedDataParsed.Permisos.Count);
            Assert.AreEqual("Role1", securedDataParsed.Permisos[0]);
            Assert.AreEqual("Role2", securedDataParsed.Permisos[1]);
        }

        private string GetResponse(string url, object parameters = null, string cookieValue = null)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            //httpWebRequest.ContentLength = 999999999999;
            //httpWebRequest.Expect = "application/json";

            if (cookieValue != null)
            {
                if (httpWebRequest.CookieContainer == null)
                {
                    httpWebRequest.CookieContainer = new CookieContainer();
                }

                httpWebRequest.CookieContainer.Add(new Uri("localhost:81"), new Cookie(TokenWebSecurityConfigurations.CookieName, cookieValue));
            }

            if (parameters != null)
            {
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    var json = JsonConvert.SerializeObject(parameters);

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }

            if (!httpWebRequest.HaveResponse)
            {
                return null;
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                return result;
            }
        }

        private string GetCookies(string url, object parameters)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = JsonConvert.SerializeObject(parameters);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            return httpResponse.Cookies[TokenWebSecurityConfigurations.CookieName]?.Value;
        }
    }
}

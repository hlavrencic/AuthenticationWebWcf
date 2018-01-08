using System;
using AuthenticationWebWcf.UsageExample.WebServiceExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestingTools.WcfSelfHost;

namespace AuthenticationWebWcf.UsageExample
{
    [TestClass]
    public class WebTransportTest
    {
        private SelfHostWcf host;

        private WcfClient client;

        private IService2 service2;
        private Mock<ILog> logMock;

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

            
            service2 = client.CreateClient<IService2>("IService2");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            host.Dispose();
            client.Dispose();
        }

        [TestMethod]
        public void TestMethod1()
        {
            SecuredData securedDataParsed = null;
            logMock.Setup(s => s.Log(It.IsAny<SecuredData>())).Callback<SecuredData>(c => securedDataParsed = c);

            service2.CallWebService();



            Assert.AreEqual("00000000-0000-0000-0000-000000000000", securedDataParsed.Guid);
            Assert.AreEqual("Client1", securedDataParsed.Client);
            Assert.AreEqual(new DateTime(2017, 1, 1), securedDataParsed.FechaExpiracion);
            Assert.AreEqual("Name1", securedDataParsed.Nombre);
            Assert.AreEqual(2, securedDataParsed.Permisos.Count);
            Assert.AreEqual("Role1", securedDataParsed.Permisos[0]);
            Assert.AreEqual("Role2", securedDataParsed.Permisos[1]);
        }
    }
}

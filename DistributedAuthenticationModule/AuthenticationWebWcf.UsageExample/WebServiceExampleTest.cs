﻿using System;
using AuthenticationWebWcf.UsageExample.WebServiceExample;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestingTools.WcfSelfHost;

namespace AuthenticationWebWcf.UsageExample
{
    [TestClass]
    public class WebServiceExampleTest
    {
        private SelfHostWcf host;

        private WcfClient client;

        private IService1 service;

        private Mock<ILog> logMock;

        [TestInitialize]
        public void Init()
        {
            logMock = new Mock<ILog>();

            client = new WcfClient();

            var provider = new ServiceProvider(logMock.Object, client);
            host = new SelfHostWcf(provider);
            
            host.Init<IService1>();
            
            service = client.CreateClient<IService1>("IService1Fixed");
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
            //var guid = new Guid();
            //var securedDataSent = new SecuredData()
            //{
            //    Client = "Client1",
            //    FechaExpiracion = new DateTime(2017,1,1),
            //    Guid = guid.ToString(),
            //    Nombre = "Name1",
            //    Permisos = new []{ "Role1" , "Role2" }
            //};

            SecuredData securedDataParsed = null;
            logMock.Setup(s => s.Log(It.IsAny<SecuredData>())).Callback<SecuredData>(c => securedDataParsed = c);

            service.Method1();

            

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

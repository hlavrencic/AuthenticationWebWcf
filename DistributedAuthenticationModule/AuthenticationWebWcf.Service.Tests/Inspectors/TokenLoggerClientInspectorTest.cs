using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using AuthenticationWebWcf.Web.Helpers;
using AuthenticationWebWcf.WebWcfClient.Inspectors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AuthenticationWebWcf.Service.Tests.Inspectors
{
    [TestClass]
    public class TokenLoggerClientInspectorTest
    {
        public WebTokenClientMessageInspector Service { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Service = new WebTokenClientMessageInspector();
        }

        [TestMethod]
        public void TestBeforeSendRequestConTokenAgregaHeader()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, "http://tempuri.org", string.Empty),
                new HttpResponse(new StringWriter()));
            HttpContextUserAuthentication.Token = "7CF0876E-6056-4B72-B3F3-C859A3882AEC-07F4";

            var clientChannelMock = new Mock<IClientChannel>();
            var message = Message.CreateMessage(MessageVersion.Default, "Test");

            Service.BeforeSendRequest(ref message, clientChannelMock.Object);
            var headerIndex = message.Headers.FindHeader("Token", string.Empty);
            Assert.IsTrue(headerIndex >= 0);
            Assert.AreEqual("7CF0876E-6056-4B72-B3F3-C859A3882AEC-07F4", message.Headers.GetHeader<string>(headerIndex));
        }

        [TestMethod]
        public void TestBeforeSendRequestSinTokenNoAgregaHeader()
        {
            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, "http://tempuri.org", string.Empty),
                new HttpResponse(new StringWriter()));

            var clientChannelMock = new Mock<IClientChannel>();
            var message = Message.CreateMessage(MessageVersion.Default, "Test");

            Service.BeforeSendRequest(ref message, clientChannelMock.Object);
            var headerIndex = message.Headers.FindHeader("Token", string.Empty);
            Assert.IsTrue(headerIndex == -1);
        }
    }
}

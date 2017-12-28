using System.Text;
using AuthenticationWebWcf.Common.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationWebWcf.Common.Tests.Crypto
{
    [TestClass]
    public class JsonWebTokenTest
    {
        public JsonWebToken Service { get; set; }

        [TestMethod]
        public void JsonWebTokenEncondeDecode()
        {
            var payload =
                new
                {
                    Propiedad1 = "valor1",
                    Arr1 = new[] {new {Prop1 = "prop1", Prop2 = "prop2"}, new {Prop1 = "prop1", Prop2 = "prop2"}}
                }.ToJson();
            var key = Encoding.UTF8.GetBytes("key1");
            var algoritm = JwtHashAlgorithm.Hs512;

            var crifrado = Service.Encode(payload, key, algoritm);
            var decrifrado = Service.Decode(crifrado, key);

            Assert.AreEqual(payload, decrifrado);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Service = new JsonWebToken();
        }
    }
}

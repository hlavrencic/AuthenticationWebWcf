using System;
using AuthenticationWebWcf.Common.Crypto;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AuthenticationWebWcf.Common.Tests.Crypto
{
    [TestClass]
    public class TokenGeneratorTest
    {
        [TestMethod]
        public void TestGenerateAndValidate()
        {
            var token = TokenGenerator.New();
            var token2 = TokenGenerator.New();
            Assert.IsTrue(TokenGenerator.IsValid(token));
            Assert.IsTrue(TokenGenerator.IsValid(token2));
        }

        [TestMethod]
        public void TestGenerateNotNull()
        {
            var token = TokenGenerator.New();
            Assert.IsNotNull(token);
        }

        [TestMethod]
        public void TestValidateNullInvalid()
        {
            Assert.IsFalse(TokenGenerator.IsValid(null));
        }

        [TestMethod]
        public void TestValidateSimpleGuidInvalid()
        {
            Assert.IsFalse(TokenGenerator.IsValid(Guid.NewGuid().ToString()));
        }

        [TestMethod]
        public void TestValidateStringInvalid()
        {
            Assert.IsFalse(TokenGenerator.IsValid("123123hola"));
        }
    }
}

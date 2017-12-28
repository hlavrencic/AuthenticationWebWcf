using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Interfaces;

namespace AuthenticationWebWcf.Web.Business
{
    public class AutenticationConverterWithAppKey : IAutenticationConverterWithAppKey
    {
        private readonly IAutenticationConverter autenticationConverter;
        private readonly IJsonWebTokenKeyConfig keyConfig;

        public AutenticationConverterWithAppKey(IAutenticationConverter autenticationConverter, IJsonWebTokenKeyConfig keyConfig)
        {
            this.autenticationConverter = autenticationConverter;
            this.keyConfig = keyConfig;
        }

        public string Encrypt(object dto)
        {
            var key = keyConfig.GetTokenKey();
            return autenticationConverter.Encrypt(dto, key);
        }

        public T Decrypt<T>(string token) where T : AuthenticatedDto
        {
            var key = keyConfig.GetTokenKey();
            return autenticationConverter.Decrypt<T>(token, key);
        }
    }
}

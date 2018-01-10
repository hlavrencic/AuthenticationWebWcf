using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Interfaces;

namespace AuthenticationWebWcf.Common
{
    public class AutenticationConverterPropKey : IAutenticationConverterWithAppKey
    {
        private readonly IAutenticationConverter autenticationConverter;

        public AutenticationConverterPropKey(IAutenticationConverter autenticationConverter)
        {
            this.autenticationConverter = autenticationConverter;
        }

        public string Key { get; set; }

        public string Encrypt(object dto)
        {
            return autenticationConverter.Encrypt(dto, Key);
        }

        public T Decrypt<T>(string token) where T : AuthenticatedDto
        {
            return autenticationConverter.Decrypt<T>(token, Key);
        }
    }
}

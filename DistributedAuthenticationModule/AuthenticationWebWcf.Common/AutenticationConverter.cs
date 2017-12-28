using AuthenticationWebWcf.Common.Crypto;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Exceptions;
using AuthenticationWebWcf.Common.Interfaces;

namespace AuthenticationWebWcf.Common
{
    public class AutenticationConverter : IAutenticationConverter
    {
        private const JwtHashAlgorithm Algorithm = JwtHashAlgorithm.Hs512;

        private readonly IJsonWebToken jsonWebToken;
        private readonly ITimeProvider timeProvider;

        public AutenticationConverter(IJsonWebToken jsonWebToken, ITimeProvider timeProvider)
        {
            this.jsonWebToken = jsonWebToken;
            this.timeProvider = timeProvider;
        }

        public string Encrypt(object dto, string key)
        {
            var payload = dto.ToJson();
            return jsonWebToken.Encode(payload, key, Algorithm);
        }

        public T Decrypt<T>(string token, string key) where T : AuthenticatedDto
        {
            var payload = jsonWebToken.Decode(token, key);
            var usuarioAutenticado = payload.FromJson<T>();

            if (usuarioAutenticado.FechaExpiracion < timeProvider.GetDateTime())
            {
                throw new ExpiredException(usuarioAutenticado.FechaExpiracion);
            }

            return usuarioAutenticado;
        }
    }
}

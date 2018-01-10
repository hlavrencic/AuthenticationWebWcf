using System.Collections.Generic;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Service.Helpers;

namespace AuthenticationWebWcf.Web.Business
{
    public class AuthenticatedUserReader<T> : IAuthenticatedUserReader<T> where T : AuthenticatedDto
    {
        private readonly IAutenticationConverterWithAppKey autenticationConverter;

        public AuthenticatedUserReader(IAutenticationConverterWithAppKey autenticationConverter)
        {
            this.autenticationConverter = autenticationConverter;
        }

        public KeyValuePair<string, T> GetUser()
        {
            var token = HttpContextUserAuthentication.Token;
            return GetUser(token);
        }

        public KeyValuePair<string, T> GetUser(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return new KeyValuePair<string, T>();
            }

            var usuarioAutenticado = autenticationConverter.Decrypt<T>(token);
            return new KeyValuePair<string, T>(token, usuarioAutenticado);
        }
    }
}

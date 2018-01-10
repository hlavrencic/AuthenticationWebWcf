using System;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.UsageExample.WebServiceExample;
using AuthenticationWebWcf.WebUsageExample.Model;

namespace AuthenticationWebWcf.WebUsageExample.External
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAutenticationConverterWithAppKey converter;

        public AuthenticationService(IAutenticationConverterWithAppKey converter)
        {
            this.converter = converter;
        }

        #region Implementation of IAuthenticationService

        public string Auth(UserLoginModel login)
        {
            // Authenticate
            var dto = new SecuredData()
            {
                FechaExpiracion = DateTime.Now.AddDays(1),
                Guid = login.User.PadLeft(32, '0'),
                Nombre = login.User
            };

            //Create token
            var token = converter.Encrypt(dto);
            return token;
        }

        #endregion
    }
}
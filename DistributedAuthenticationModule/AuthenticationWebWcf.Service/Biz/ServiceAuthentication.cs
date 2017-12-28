using System;
using System.ServiceModel;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Exceptions;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Service.DataContracts;

namespace AuthenticationWebWcf.Service.Biz
{
    public class ServiceAuthentication<T> : IServiceAuthentication<T> where T : AuthenticatedDto
    {
        public const string ExFirmaInvalida = "Token con firma inválida.";
        public const string ExSerializacion = "Token inválido.";
        public const string ExVencido = "El Token expiró el {0}";

        private readonly IAutenticationConverter autenticationConverter;

        public ServiceAuthentication(IAutenticationConverter autenticationConverter)
        {
            this.autenticationConverter = autenticationConverter;
        }

        public static string ExVencidoFecha(DateTime fechaVencimiento)
        {
            return string.Format(ExVencido, fechaVencimiento);
        }

        public static FaultException CrearExcepcion(string razon)
        {
            throw new FaultException<UnauthorizedAccessFault>(new UnauthorizedAccessFault { ErrorList = new[] { razon } }, razon);
        }

        public T GetAuthenticatedData(string token, string tokenKey)
        {
            if (string.IsNullOrEmpty(token))
            {
                throw CrearExcepcion("No se encontró un header que contenga el Token.");
            }

            try
            {
                return autenticationConverter.Decrypt<T>(token, tokenKey);
            }
            catch (ExpiredException ex)
            {
                throw CrearExcepcion(ExVencidoFecha(ex.Expired));
            }
            catch (SignatureVerificationException)
            {
                throw CrearExcepcion(ExFirmaInvalida);
            }
            catch (SerializationException)
            {
                throw CrearExcepcion(ExSerializacion);
            }
        }
    }
}

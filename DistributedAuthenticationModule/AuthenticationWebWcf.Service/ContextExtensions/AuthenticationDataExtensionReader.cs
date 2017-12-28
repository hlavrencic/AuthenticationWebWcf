using System;
using System.ServiceModel;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Service.Biz;
using AuthenticationWebWcf.Service.DataContracts;

namespace AuthenticationWebWcf.Service.ContextExtensions
{
    public class AuthenticationDataExtensionReader<T> : IAuthenticationDataExtensionReader<T> where T : AuthenticatedDto
    {
        public const string ExFirmaInvalida = "Token con firma inválida.";
        public const string ExSerializacion = "Token inválido.";
        public const string ExVencido = "El Token expiró el {0}";

        private readonly IServiceAuthentication<T> serviceAuthentication;
        private readonly ITokenDataExtensionReader tokenDataExtensionReader;

        public AuthenticationDataExtensionReader(IServiceAuthentication<T> serviceAuthentication, ITokenDataExtensionReader tokenDataExtensionReader)
        {
            this.serviceAuthentication = serviceAuthentication;
            this.tokenDataExtensionReader = tokenDataExtensionReader;
        }

        public static string ExVencidoFecha(DateTime fechaVencimiento)
        {
            return string.Format(ExVencido, fechaVencimiento);
        }

        public static FaultException CrearExcepcion(string razon)
        {
            throw new FaultException<UnauthorizedAccessFault>(new UnauthorizedAccessFault { ErrorList = new[] { razon } }, razon);
        }

        public T GetUser(IExtensibleObject<OperationContext> operationContext)
        {
            var dataExtension = tokenDataExtensionReader.GetCurrentExtension(operationContext);
            if (dataExtension == null)
            {
                return null;
            }

            return serviceAuthentication.GetAuthenticatedData(dataExtension.Token, dataExtension.TokenKey);
        }
    }
}

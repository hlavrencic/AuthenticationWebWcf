using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Service.DataContracts;

namespace AuthenticationWebWcf.Service.Biz
{
    public class ActionValidation<T> : IActionValidation<T> where T : AuthenticatedDto
    {
        private readonly IServiceAuthentication<T> authenticationDataExtensionReader;

        public ActionValidation(IServiceAuthentication<T> authenticationDataExtensionReader)
        {
            this.authenticationDataExtensionReader = authenticationDataExtensionReader;
        }

        public T Validate(string token, string tokenKey, IList<string> permisosRequeridos)
        {
            var usuarioAutenticado = authenticationDataExtensionReader.GetAuthenticatedData(token, tokenKey);
            if (usuarioAutenticado == null)
            {
                return null;
            }

            // Verifico si hay permisos para validar.
            if (permisosRequeridos == null)
            {
                return usuarioAutenticado;
            }

            var autorizado = permisosRequeridos.Any(a => usuarioAutenticado.Permisos.Contains(a));
            if (autorizado)
            {
                return usuarioAutenticado;
            }

            var msgError = "No se cumplen los siguientes permisos: " + string.Join(", ", permisosRequeridos);
            throw new FaultException<UnauthorizedAccessFault>(new UnauthorizedAccessFault { ErrorList = new[] { msgError } }, msgError);
        }
    }
}

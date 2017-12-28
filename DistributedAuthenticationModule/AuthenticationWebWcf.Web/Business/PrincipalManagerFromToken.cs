using System.Linq;
using System.Security.Principal;
using System.Web;
using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Web.Business
{
    public class PrincipalManagerFromToken : IPrincipalManagerFromToken
    {
        private readonly IAuthenticatedUserManager<AuthenticatedDto> authenticatedUserManager;

        public PrincipalManagerFromToken(IAuthenticatedUserManager<AuthenticatedDto> authenticatedUserManager)
        {
            this.authenticatedUserManager = authenticatedUserManager;
        }

        public IPrincipal GetPrincipal(HttpRequestBase request)
        {
            var usuarioAutenticado = authenticatedUserManager.ReadUser(request);
            return GetPrincipal(usuarioAutenticado.Value);
        }

        private IPrincipal GetPrincipal(AuthenticatedDto usuarioAutenticado)
        {
            if (usuarioAutenticado == null)
            {
                return null;
            }

            var roles = GetRoles(usuarioAutenticado);
            return new GenericPrincipal(GetIdentity(usuarioAutenticado.Nombre), roles);
        }

        private IIdentity GetIdentity(string nombre)
        {
            return new GenericIdentity(nombre);
        }

        private string[] GetRoles(AuthenticatedDto usuarioAutenticado)
        {
            return usuarioAutenticado.Permisos.Select(s => s.ToString()).ToArray();
        }
    }
}

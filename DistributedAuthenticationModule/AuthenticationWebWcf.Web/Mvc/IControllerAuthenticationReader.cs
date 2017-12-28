using System.Web.Mvc;

namespace AuthenticationWebWcf.Web.Mvc
{
    public interface IControllerAuthenticationReader
    {
        bool IsInRole(Controller controller, string permisos);
    }
}

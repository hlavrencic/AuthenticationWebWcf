using System.Web.Mvc;

namespace AuthenticationWebWcf.Web.Mvc
{
    public class ControllerAuthenticationReader : IControllerAuthenticationReader
    {
        public bool IsInRole(Controller controller, string permisos)
        {
            return controller.User.IsInRole(permisos);
        }
    }
}
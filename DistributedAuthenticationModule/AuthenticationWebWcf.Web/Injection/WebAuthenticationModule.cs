using AuthenticationWebWcf.Common;
using Ninject.Modules;

namespace AuthenticationWebWcf.Web.Injection
{
    public class WebAuthenticationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.BindAssembly<WebAuthenticationModule>();
        }
    }
}

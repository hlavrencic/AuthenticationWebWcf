using AuthenticationWebWcf.Common;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.IService.Inspectors;
using AuthenticationWebWcf.Service.Inspectors;
using Ninject.Modules;

namespace AuthenticationWebWcf.Service.Providers
{
    public class ServiceAuthenticationModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.BindAssembly<ServiceAuthenticationModule>();

            if (Kernel != null)
            {
                Kernel.Rebind<ITokenDispatchMessageInspector>().To<TokenDispatchMessageInspector<AuthenticatedDto>>();
            }
        }
    }
}

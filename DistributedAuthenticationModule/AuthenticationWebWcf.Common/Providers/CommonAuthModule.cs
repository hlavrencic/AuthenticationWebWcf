using AuthenticationWebWcf.Common.Crypto;
using AuthenticationWebWcf.Common.Interfaces;
using Ninject.Modules;

namespace AuthenticationWebWcf.Common.Providers
{
    public class CommonAuthModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProvider>().To<BehaviorServiceProvider>();
            Bind<IJsonWebToken>().To<JsonWebToken>();
            Bind<IFixedToken>().To<FixedToken>();
            Bind<IAutenticationConverter>().To<AutenticationConverter>();
            Bind<ITimeProvider>().To<TimeProvider>();
        }
    }
}

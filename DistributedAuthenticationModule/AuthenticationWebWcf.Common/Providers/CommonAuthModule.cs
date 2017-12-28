using Ninject.Modules;

namespace AuthenticationWebWcf.Common.Providers
{
    public class CommonAuthModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.BindAssembly<CommonAuthModule>();

            Bind<IProvider>().ToMethod(c => BehaviorServiceProvider.Current());
        }
    }
}

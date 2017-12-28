using AuthenticationWebWcf.Common.Providers;
using Ninject;

namespace AuthenticationWebWcf.Service.Providers
{
    public class ServiceProviderInitializer
    {
        public static IProvider Intialize()
        {
            if (BehaviorServiceProvider.Current() != null)
            {
                return BehaviorServiceProvider.Current();
            }

            var kernel = new StandardKernel();
            kernel.Load<CommonAuthModule>();
            kernel.Load<ServiceAuthenticationModule>();
            BehaviorServiceProvider.Register(kernel);

            return BehaviorServiceProvider.Current();
        }
    }
}

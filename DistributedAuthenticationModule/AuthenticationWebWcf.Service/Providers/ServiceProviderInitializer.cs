using System;
using System.Collections.Generic;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Config;
using Ninject;

namespace AuthenticationWebWcf.Service.Providers
{
    public class ServiceProviderInitializer
    {
        private static IKernel CreateProvider()
        {
            var kernel = new StandardKernel();
            var commonModule = kernel.Get<CommonAuthModule>();
            kernel.Load(commonModule);
            kernel.Load<ServiceAuthenticationModule>();
            return kernel;
        }

        public static IProvider Intialize()
        {
            var kernel = CreateProvider();
            return BehaviorServiceProvider.Create(kernel) ;
        }
        
        public static void RebindWithConfig(IProvider provider, ReBindElementCollection rebinds)
        {
            var dic = GetValidBinds(rebinds);
            foreach (var rebind in dic)
            {
                provider.ReBindTo(rebind.Value, rebind.Key);
            }
        }

        public static IDictionary<Type, Type> GetValidBinds(ReBindElementCollection rebinds)
        {
            var result = new Dictionary<Type,Type>();

            foreach (var rebind in rebinds)
            {
                var rebindCast = (ReBindElement)rebind;
                var interfaceType = Type.GetType(rebindCast.InterfaceType, true);
                var implementationType = Type.GetType(rebindCast.ImplementationType, true);

                result.Add(interfaceType, implementationType);
            }

            return result;
        }
    }
}

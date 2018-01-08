using System;
using Ninject;

namespace AuthenticationWebWcf.Common.Providers
{
    // Usar solo en caso de emergencia!!
    // Por ejemplo: para obtener dependencias dentro de un behavior
    public class BehaviorServiceProvider : IProvider
    {
        private readonly IKernel kernel;

        private BehaviorServiceProvider(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public static IProvider Create(IKernel kernel)
        {
            return new BehaviorServiceProvider(kernel);
        }

        public TService Get<TService>()
        {
            return kernel.Get<TService>();
        }

        public TService Get<TService>(string namedBinding)
        {
            return kernel.Get<TService>(namedBinding);
        }

        public void ReBindTo(Type implementationType, params Type[] interfaceTypes)
        {
            foreach (var interfaceType in interfaceTypes)
            {
                kernel.Unbind(interfaceType);
            }
            
            kernel.Bind(interfaceTypes).To(implementationType);
        }

        public void ReBindTo<TInterface,TImplementation>()
            where TImplementation : TInterface
        {
            kernel.Unbind<TInterface>();
            kernel.Bind<TInterface>().To<TImplementation>();
        }
    }
}

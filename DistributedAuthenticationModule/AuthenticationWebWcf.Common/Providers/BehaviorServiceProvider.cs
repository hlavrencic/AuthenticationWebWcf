using Ninject;

namespace AuthenticationWebWcf.Common.Providers
{
    // Usar solo en caso de emergencia!!
    // Por ejemplo: para obtener dependencias dentro de un behavior
    public class BehaviorServiceProvider : IProvider
    {
        private static IProvider current;

        private readonly IKernel kernel;

        private BehaviorServiceProvider(IKernel kernel)
        {
            this.kernel = kernel;
        }

        public static IProvider Current()
        {
            return current;
        }

        public static void Register(IKernel kernel)
        {
            current = new BehaviorServiceProvider(kernel);
        }

        public TService Get<TService>()
        {
            return kernel.Get<TService>();
        }

        public TService Get<TService>(string namedBinding)
        {
            return kernel.Get<TService>(namedBinding);
        }
    }
}

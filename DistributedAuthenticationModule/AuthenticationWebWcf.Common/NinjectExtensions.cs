using Ninject;
using Ninject.Extensions.Conventions.Syntax;
using Ninject.Extensions.Conventions;

namespace AuthenticationWebWcf.Common
{
    public static class NinjectExtensions
    {
        public static void BindAssembly<T>(this IKernel kernel)
        {
            kernel.Bind(BindConfig<T>);
        }

        public static void BindConfig<T>(this IFromSyntax sintax)
        {
            var from = sintax.FromAssemblyContaining<T>();
            var clasess = from.SelectAllClasses();
            clasess.BindDefaultInterface();
        }

        public static void SetMock<TInterface>(this IKernel kernel, TInterface implementation)
        {
            kernel.Unbind<TInterface>();
            kernel.Bind<TInterface>().ToConstant(implementation).InSingletonScope();
        }
    }
}

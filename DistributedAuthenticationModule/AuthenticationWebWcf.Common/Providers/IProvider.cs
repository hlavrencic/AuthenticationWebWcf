using System;

namespace AuthenticationWebWcf.Common.Providers
{
    public interface IProvider
    {
        TService Get<TService>();

        TService Get<TService>(string namedBinding);

        void ReBindTo(Type implementationType, params Type[] interfaceTypes);

        void ReBindTo<TInterface, TImplementation>()
            where TImplementation : TInterface;
    }
}
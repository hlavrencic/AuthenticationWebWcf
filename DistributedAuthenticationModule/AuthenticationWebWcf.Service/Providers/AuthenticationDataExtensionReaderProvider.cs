using System;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.ContextExtensions;

namespace AuthenticationWebWcf.Service.Providers
{
    public class AuthenticationDataExtensionReaderProvider
    {
        private IProvider provider;

        public AuthenticationDataExtensionReaderProvider()
        {
        }

        internal AuthenticationDataExtensionReaderProvider(IProvider provider)
        {
            this.provider = provider;
        }

        public IAuthenticationDataExtensionReader<T> CreateAuthenticationDataExtensionReader<T>() where T : AuthenticatedDto
        {
            if (provider == null)
            {
                provider = ServiceProviderInitializer.Intialize();
            }
            
            return provider.Get<IAuthenticationDataExtensionReader<T>>();
        }
        
        public void RebindTo<TInterface,TImplementation>()
            where TImplementation : TInterface
        {
            if (provider == null)
            {
                provider = ServiceProviderInitializer.Intialize();
            }

            provider.ReBindTo<TInterface,TImplementation>();
        }
    }
}

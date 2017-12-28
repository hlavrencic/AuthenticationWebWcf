using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.ContextExtensions;

namespace AuthenticationWebWcf.Service.Providers
{
    public class AuthenticationDataExtensionReaderProvider
    {
        private readonly IProvider provider;

        public AuthenticationDataExtensionReaderProvider()
        {
            provider = ServiceProviderInitializer.Intialize();
        }

        internal AuthenticationDataExtensionReaderProvider(IProvider provider)
        {
            this.provider = provider;
        }

        public IAuthenticationDataExtensionReader<T> CreateAuthenticationDataExtensionReader<T>() where T : AuthenticatedDto
        {
            return provider.Get<IAuthenticationDataExtensionReader<T>>();
        }
    }
}

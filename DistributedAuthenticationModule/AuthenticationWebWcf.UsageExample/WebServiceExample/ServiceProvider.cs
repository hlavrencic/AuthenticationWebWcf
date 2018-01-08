using System;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Service.Providers;
using AuthenticationWebWcf.UsageExample.ImplementationOverrides;
using TestingTools.WcfSelfHost;

namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    public class ServiceProvider : IServiceProvider
    {
        private readonly ILog logger;
        private readonly WcfClient client;

        public ServiceProvider(
            ILog logger,
            WcfClient client)
        {
            this.logger = logger;
            this.client = client;
        }

        #region Implementation of IServiceProvider

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IService1))
            {
                var provider = new AuthenticationDataExtensionReaderProvider();
                provider.RebindTo<ITimeProvider, TimeProviderOverride>();

                return new Service1(provider.CreateAuthenticationDataExtensionReader<SecuredData>(), logger);
            }

            if (serviceType == typeof(IService2))
            {
                var service = client.CreateClient<IService1>("IService1");

                return new Service2(service);
            }

            return null;
        }

        #endregion
    }
}

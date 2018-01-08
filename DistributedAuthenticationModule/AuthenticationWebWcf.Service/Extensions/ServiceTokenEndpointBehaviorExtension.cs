using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Config;
using AuthenticationWebWcf.Service.Inspectors;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class ServiceTokenEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private const string ReBindElementCollectionName = "ReBindElementCollection";

        private IProvider provider;

        public ServiceTokenEndpointBehaviorExtension()
        {
        }

        public ServiceTokenEndpointBehaviorExtension(IProvider provider)
        {
            this.provider = provider;
        }

        public override Type BehaviorType
        {
            get { return typeof(TokenEndpointBehavior<ServiceTokenClientMessageInspector>); }
        }

        [ConfigurationProperty(ReBindElementCollectionName, IsDefaultCollection = false)]
        public ReBindElementCollection ReBindElementCollection
        {
            get { return (ReBindElementCollection)base[ReBindElementCollectionName]; }
            set { base[ReBindElementCollectionName] = value; }
        }

        protected override object CreateBehavior()
        {
            if (provider == null)
            {
                provider = ServiceProviderInitializer.Intialize();
            }

            ServiceProviderInitializer.RebindWithConfig(provider, ReBindElementCollection);

            var behavior = provider.Get<TokenEndpointBehavior<ServiceTokenClientMessageInspector>>();
            return behavior;
        }
    }
}

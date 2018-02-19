using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Config;
using AuthenticationWebWcf.Service.Providers;
using AuthenticationWebWcf.WebWcfClient.Inspectors;

namespace AuthenticationWebWcf.WebWcfClient.Extensions
{
    public class WebTokenEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private const string ReBindElementCollectionName = "ReBindElementCollection";

        private IProvider provider;

        public WebTokenEndpointBehaviorExtension()
        {
        }

        public WebTokenEndpointBehaviorExtension(IProvider provider)
        {
            this.provider = provider;
        }

        public override Type BehaviorType
        {
            get { return typeof(TokenEndpointBehavior<WebTokenClientMessageInspector>); }
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

            var behavior = provider.Get<TokenEndpointBehavior<WebTokenClientMessageInspector>>();
            return behavior;
        }
    }
}

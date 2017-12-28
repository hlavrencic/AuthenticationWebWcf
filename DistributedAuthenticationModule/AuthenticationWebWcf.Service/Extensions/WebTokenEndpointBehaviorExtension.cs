using System;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Inspectors;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class WebTokenEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private readonly IProvider provider;

        public WebTokenEndpointBehaviorExtension()
        {
            ServiceProviderInitializer.Intialize();
            provider = BehaviorServiceProvider.Current();
        }

        public WebTokenEndpointBehaviorExtension(IProvider provider)
        {
            this.provider = provider;
        }

        public override Type BehaviorType
        {
            get { return typeof(TokenEndpointBehavior<WebTokenClientMessageInspector>); }
        }

        protected override object CreateBehavior()
        {
            var behavior = provider.Get<TokenEndpointBehavior<WebTokenClientMessageInspector>>();
            return behavior;
        }
    }
}

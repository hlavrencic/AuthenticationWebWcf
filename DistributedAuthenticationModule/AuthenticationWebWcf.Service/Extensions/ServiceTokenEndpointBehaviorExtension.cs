using System;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Inspectors;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class ServiceTokenEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private readonly IProvider provider;

        public ServiceTokenEndpointBehaviorExtension()
        {
            ServiceProviderInitializer.Intialize();
            provider = BehaviorServiceProvider.Current();
        }

        public ServiceTokenEndpointBehaviorExtension(IProvider provider)
        {
            this.provider = provider;
        }

        public override Type BehaviorType
        {
            get { return typeof(TokenEndpointBehavior<ServiceTokenClientMessageInspector>); }
        }

        protected override object CreateBehavior()
        {
            var behavior = provider.Get<TokenEndpointBehavior<ServiceTokenClientMessageInspector>>();
            return behavior;
        }
    }
}

using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Inspectors;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class FixedTokenEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private const string TokenConfigName = "Token";

        private readonly IProvider provider;

        public FixedTokenEndpointBehaviorExtension()
        {
            ServiceProviderInitializer.Intialize();
            provider = BehaviorServiceProvider.Current();
        }

        public FixedTokenEndpointBehaviorExtension(IProvider provider)
        {
            this.provider = provider;
        }

        [ConfigurationProperty(TokenConfigName)]
        public string Token
        {
            get { return (string)base[TokenConfigName]; }
            set { base[TokenConfigName] = value; }
        }

        public override Type BehaviorType
        {
            get { return typeof(TokenEndpointBehavior<FixedTokenClientMessageInspector>); }
        }

        protected override object CreateBehavior()
        {
            provider.Get<IFixedToken>().SetToken(Token);
            var behavior = provider.Get<TokenEndpointBehavior<FixedTokenClientMessageInspector>>();
            return behavior;
        }
    }
}

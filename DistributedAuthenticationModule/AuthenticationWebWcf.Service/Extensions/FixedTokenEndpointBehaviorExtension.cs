using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Config;
using AuthenticationWebWcf.Service.Inspectors;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class FixedTokenEndpointBehaviorExtension : BehaviorExtensionElement
    {
        private const string TokenConfigName = "Token";

        private const string DateName = "Date";

        private const string ReBindElementCollectionName = "ReBindElementCollection";

        private IProvider provider;

        public FixedTokenEndpointBehaviorExtension()
        {
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

        [ConfigurationProperty(DateName)]
        public string DateStr
        {
            get { return (string)base[DateName]; }
            set { base[DateName] = value; }
        }

        public DateTime Date => DateTime.Parse(DateStr);

        public override Type BehaviorType
        {
            get { return typeof(TokenEndpointBehavior<FixedTokenClientMessageInspector>); }
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

            provider.Get<IFixedToken>().SetToken(Token);
            var behavior = provider.Get<TokenEndpointBehavior<FixedTokenClientMessageInspector>>();
            return behavior;
        }
    }
}

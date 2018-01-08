using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Config;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class ServiceTokenDispatchBehaviorExtension : BehaviorExtensionElement
    {
        private const string TokenKeyConfigName = "Key";

        private const string ReBindElementCollectionName = "ReBindElementCollection";

        private IProvider provider;

        public ServiceTokenDispatchBehaviorExtension()
        {
        }

        /// <summary>
        /// For testing
        /// </summary>
        /// <param name="provider">Mock provider</param>
        public ServiceTokenDispatchBehaviorExtension(IProvider provider)
        {
            this.provider = provider;
        }

        public override Type BehaviorType
        {
            get { return typeof(TokenValidationServiceBehaviorAttribute); }
        }

        [ConfigurationProperty(TokenKeyConfigName)]
        public string TokenKey
        {
            get { return (string)base[TokenKeyConfigName]; }
            set { base[TokenKeyConfigName] = value; }
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

            var behavior = provider.Get<TokenValidationServiceBehaviorAttribute>();
            behavior.TokenKey = TokenKey;
            return behavior;
        }
    }
}

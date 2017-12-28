using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Providers;

namespace AuthenticationWebWcf.Service.Extensions
{
    public class ServiceTokenDispatchBehaviorExtension : BehaviorExtensionElement
    {
        private const string TokenKeyConfigName = "Key";

        private readonly IProvider provider;

        public ServiceTokenDispatchBehaviorExtension()
        {
            ServiceProviderInitializer.Intialize();
            provider = BehaviorServiceProvider.Current();
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

        protected override object CreateBehavior()
        {
            var behavior = provider.Get<TokenValidationServiceBehaviorAttribute>();
            behavior.TokenKey = TokenKey;
            return behavior;
        }
    }
}

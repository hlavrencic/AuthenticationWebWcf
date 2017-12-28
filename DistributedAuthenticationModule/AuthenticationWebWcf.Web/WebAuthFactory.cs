﻿using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Common.Providers;
using AuthenticationWebWcf.Web.Business;
using AuthenticationWebWcf.Web.Injection;
using AuthenticationWebWcf.Web.Mvc;
using Ninject;

namespace AuthenticationWebWcf.Web
{
    public class WebAuthFactory
    {
        private readonly IProvider provider;

        internal WebAuthFactory(IProvider provider)
        {
            this.provider = provider;
        }

        public WebAuthFactory()
        {
            provider = BehaviorServiceProvider.Current();
            if (provider != null)
            {
                return;
            }

            var kernel = new StandardKernel();
            kernel.Load<CommonAuthModule>();
            kernel.Load<WebAuthenticationModule>();
            BehaviorServiceProvider.Register(kernel);

            provider = BehaviorServiceProvider.Current();
        }

        public GlobalAuthenticationActionFilterAttribute CreateGlobalAuthenticationActionFilterAttribute()
        {
            return provider.Get<GlobalAuthenticationActionFilterAttribute>();
        }

        public ITokenCookieManager CreateTokenCookieManager()
        {
            return provider.Get<ITokenCookieManager>();
        }

        public IAutenticationConverterWithAppKey CreateAutenticationConverter()
        {
            return provider.Get<IAutenticationConverterWithAppKey>();
        }

        public IAuthenticatedUserReader<T> CreateAuthenticatedUserReader<T>() where T : AuthenticatedDto
        {
            return provider.Get<IAuthenticatedUserReader<T>>();
        }
    }
}

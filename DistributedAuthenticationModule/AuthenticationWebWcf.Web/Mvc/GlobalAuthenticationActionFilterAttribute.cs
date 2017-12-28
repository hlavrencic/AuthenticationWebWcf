using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using AuthenticationWebWcf.Common.Exceptions;
using AuthenticationWebWcf.Web.Business;
using Ninject.Extensions.Logging;

namespace AuthenticationWebWcf.Web.Mvc
{
    public class GlobalAuthenticationActionFilterAttribute : IAuthenticationFilter
    {
        private readonly IPrincipalManagerFromToken principalManagerFromToken;
        private readonly ILogger logger;

        public GlobalAuthenticationActionFilterAttribute(IPrincipalManagerFromToken principalManagerFromToken, ILogger logger)
        {
            this.principalManagerFromToken = principalManagerFromToken;
            this.logger = logger;
        }

        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var actionFilters = filterContext.ActionDescriptor.GetCustomAttributes(false);
            var controllerFilters = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(false);
            var filtersConcat = controllerFilters.Concat(actionFilters);
            if (filtersConcat.Any(f => f is AllowAnonymousAttribute))
            {
                return;
            }

            try
            {
                filterContext.Principal = principalManagerFromToken.GetPrincipal(filterContext.HttpContext.Request);

                if (filterContext.Principal == null)
                {
                    SetUnauthorizedResult(filterContext, "Debe autenticarse.");
                }
            }
            catch (ExpiredException ex)
            {
                SetUnauthorizedResult(filterContext, string.Format("El Token expiró el {0}", ex.Expired), ex);
            }
            catch (SignatureVerificationException ex)
            {
                SetUnauthorizedResult(filterContext, "Token con firma inválida.", ex);
            }
            catch (SerializationException ex)
            {
                SetUnauthorizedResult(filterContext, "Token inválido.", ex);
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }

        private void SetUnauthorizedResult(AuthenticationContext filterContext, string msg)
        {
            logger.Debug(msg);
            filterContext.Result = new HttpUnauthorizedResult(msg);
        }

        private void SetUnauthorizedResult(AuthenticationContext filterContext, string msg, Exception ex)
        {
            logger.Debug(msg, ex);
            filterContext.Result = new HttpUnauthorizedResult(msg);
        }
    }
}

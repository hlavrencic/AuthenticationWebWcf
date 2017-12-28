using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using AuthenticationWebWcf.Common.DataContracts;
using AuthenticationWebWcf.Service.Behaviors;
using AuthenticationWebWcf.Service.Biz;
using AuthenticationWebWcf.Service.ContextExtensions;
using AuthenticationWebWcf.Service.Helpers;

namespace AuthenticationWebWcf.Service.Inspectors
{
    public class TokenDispatchMessageInspector<T> : ITokenDispatchMessageInspector where T : AuthenticatedDto
    {
        private readonly IAuthenticationDataExtension newAuthenticationDataExtension;
        private readonly IActionValidation<T> actionValidation;

        public TokenDispatchMessageInspector(IAuthenticationDataExtension newAuthenticationDataExtension, IActionValidation<T> actionValidation)
        {
            this.newAuthenticationDataExtension = newAuthenticationDataExtension;
            this.actionValidation = actionValidation;
        }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (request.IsEmpty)
            {
                return instanceContext;
            }

            // Agrego una extension al context para luego poder consultarla en cualquier momento.
            newAuthenticationDataExtension.Token = request.Headers.GetToken();
            OperationContext.Current.Extensions.Add(newAuthenticationDataExtension);

            Authorize(OperationContext.Current, newAuthenticationDataExtension.Token, newAuthenticationDataExtension.TokenKey);

            return instanceContext;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        public void SetKey(string tokenKey)
        {
            newAuthenticationDataExtension.TokenKey = tokenKey;
        }

        private void Authorize(OperationContext ctx, string token, string tokenKey)
        {
            var op = GetOperationDescription(ctx);
            if (op == null)
            {
                return;
            }

            // Obtengo los permisos requeridos, si corresponde.
            var tokenValidator = op.Behaviors.Find<TokenValidatorOperationBehaviorAttribute>();
            var permisos = tokenValidator == null ? null : tokenValidator.GetPermisos().ToArray();

            actionValidation.Validate(token, tokenKey, permisos);
        }

        private OperationDescription GetOperationDescription(OperationContext ctx)
        {
            var hostDesc = ctx.Host.Description;
            var endpoint = hostDesc.Endpoints.Find(ctx.IncomingMessageHeaders.To);
            if (endpoint == null)
            {
                return null;
            }

            var operationName = ctx.IncomingMessageHeaders.Action.Replace(endpoint.Contract.Namespace + endpoint.Contract.Name + "/", "");
            var operation = endpoint.Contract.Operations.Find(operationName);
            return operation;
        }
    }
}

using System.ServiceModel;

namespace AuthenticationWebWcf.Service.ContextExtensions
{
    public class TokenDataExtensionReader : ITokenDataExtensionReader
    {
        public string GetToken(IExtensibleObject<OperationContext> operationContext)
        {
            var dataExtension = GetCurrentExtension(operationContext);
            return dataExtension == null ? null : dataExtension.Token;
        }

        public IAuthenticationDataExtension GetCurrentExtension(IExtensibleObject<OperationContext> operationContext)
        {
            if (operationContext == null)
            {
                return null;
            }

            var dataExtension = operationContext.Extensions.Find<IAuthenticationDataExtension>();
            return dataExtension;
        }
    }
}

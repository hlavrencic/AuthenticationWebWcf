using System.ServiceModel;

namespace AuthenticationWebWcf.Service.ContextExtensions
{
    public interface ITokenDataExtensionReader
    {
        string GetToken(IExtensibleObject<OperationContext> operationContext);

        IAuthenticationDataExtension GetCurrentExtension(IExtensibleObject<OperationContext> operationContext);
    }
}
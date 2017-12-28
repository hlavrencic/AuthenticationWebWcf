using System.ServiceModel;
using AuthenticationWebWcf.Common.DataContracts;

namespace AuthenticationWebWcf.Service.ContextExtensions
{
    public interface IAuthenticationDataExtensionReader<out T> where T : AuthenticatedDto
    {
        T GetUser(IExtensibleObject<OperationContext> operationContext);
    }
}
using System.ServiceModel;

namespace AuthenticationWebWcf.Service.ContextExtensions
{
    public interface IAuthenticationDataExtension : IExtension<OperationContext>
    {
        string Token { get; set; }

        string TokenKey { get; set; }
    }
}
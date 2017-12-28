using System.ServiceModel;

namespace AuthenticationWebWcf.Service.ContextExtensions
{
    public class AuthenticationDataExtension : IAuthenticationDataExtension
    {
        public string Token { get; set; }
        public string TokenKey { get; set; }

        public void Attach(OperationContext owner)
        {
        }

        public void Detach(OperationContext owner)
        {
        }
    }
}

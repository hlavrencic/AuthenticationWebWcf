using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthenticationWebWcf.IService.Inspectors;
using AuthenticationWebWcf.Service.ContextExtensions;
using AuthenticationWebWcf.Service.Helpers;

namespace AuthenticationWebWcf.Service.Inspectors
{
    public class ServiceTokenClientMessageInspector : ITokenClientMessageInspector
    {
        private readonly ITokenDataExtensionReader tokenDataExtensionReader;

        public ServiceTokenClientMessageInspector(ITokenDataExtensionReader tokenDataExtensionReader)
        {
            this.tokenDataExtensionReader = tokenDataExtensionReader;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var token = tokenDataExtensionReader.GetToken(OperationContext.Current);
            if (token != null)
            {
                request.Headers.SetToken(token);
            }

            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }
    }
}
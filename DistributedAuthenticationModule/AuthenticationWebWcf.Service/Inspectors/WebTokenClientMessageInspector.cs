using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthenticationWebWcf.Service.Helpers;

namespace AuthenticationWebWcf.Service.Inspectors
{
    public class WebTokenClientMessageInspector : ITokenClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var token = HttpContextUserAuthentication.Token;
            if (token != null)
            {
                request.Headers.SetToken(token);
            }

            return token;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            // No se implementa
        }
    }
}

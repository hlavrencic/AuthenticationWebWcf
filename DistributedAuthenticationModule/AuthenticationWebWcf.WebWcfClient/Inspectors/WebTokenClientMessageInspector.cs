using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthenticationWebWcf.IService.Inspectors;
using AuthenticationWebWcf.Service.Helpers;
using AuthenticationWebWcf.Web.Helpers;

namespace AuthenticationWebWcf.WebWcfClient.Inspectors
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

using System.ServiceModel;
using System.ServiceModel.Channels;
using AuthenticationWebWcf.Common.Interfaces;
using AuthenticationWebWcf.Service.Helpers;

namespace AuthenticationWebWcf.Service.Inspectors
{
    public class FixedTokenClientMessageInspector : ITokenClientMessageInspector
    {
        private readonly IFixedToken fixedToken;

        public FixedTokenClientMessageInspector(IFixedToken fixedToken)
        {
            this.fixedToken = fixedToken;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            var token = fixedToken.GetToken();
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
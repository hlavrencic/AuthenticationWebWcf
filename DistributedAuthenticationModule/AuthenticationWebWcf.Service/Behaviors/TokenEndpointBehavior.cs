using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using AuthenticationWebWcf.Service.Inspectors;

namespace AuthenticationWebWcf.Service.Behaviors
{
    public class TokenEndpointBehavior<TInspector> : IEndpointBehavior
        where TInspector : ITokenClientMessageInspector
    {
        private readonly ITokenClientMessageInspector inspector;

        public TokenEndpointBehavior(TInspector inspector)
        {
            this.inspector = inspector;
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(inspector);
        }
    }
}

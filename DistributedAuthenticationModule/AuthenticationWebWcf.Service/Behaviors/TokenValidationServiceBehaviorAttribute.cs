using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using AuthenticationWebWcf.Service.DataContracts;
using AuthenticationWebWcf.Service.Inspectors;
using Ninject.Extensions.Logging;

namespace AuthenticationWebWcf.Service.Behaviors
{
    public class TokenValidationServiceBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly ITokenDispatchMessageInspector tokenDispatchMessageInspector;
        private readonly ILogger logger;

        public TokenValidationServiceBehaviorAttribute(ITokenDispatchMessageInspector tokenDispatchMessageInspector, ILogger logger)
        {
            this.tokenDispatchMessageInspector = tokenDispatchMessageInspector;
            this.logger = logger;
        }

        public string TokenKey { get; set; }

        /// <summary>
        /// Validate if every OperationContract included in a ServiceContract related to this behavior declares PackedFault as FaultContract
        /// </summary>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (var se in serviceDescription.Endpoints)
            {
                // Must not examine any metadata endpoint.
                if (se.Contract.Name.Equals("IMetadataExchange") &&
                    se.Contract.Namespace.Equals("http://schemas.microsoft.com/2006/04/mex"))
                {
                    continue;
                }

                var type = typeof(UnauthorizedAccessFault);
                foreach (var opDesc in se.Contract.Operations)
                {
                    if (opDesc.Faults.Count != 0 && opDesc.Faults.Any(fault => fault.DetailType == type))
                    {
                        continue;
                    }

                    var msg = string.Format("{0} requires a FaultContractAttribute(typeof({1})) in each operation contract. The \"{2}\" operation contains no FaultContractAttribute.", GetType().FullName, type.FullName, opDesc.Name);
                    logger.Warn(msg);
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            tokenDispatchMessageInspector.SetKey(TokenKey);

            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var chanDisp = (ChannelDispatcher) channelDispatcherBase;
                foreach (var endpointDispatch in chanDisp.Endpoints)
                {
                    endpointDispatch.DispatchRuntime.MessageInspectors.Add(tokenDispatchMessageInspector);
                }
            }
        }
    }
}

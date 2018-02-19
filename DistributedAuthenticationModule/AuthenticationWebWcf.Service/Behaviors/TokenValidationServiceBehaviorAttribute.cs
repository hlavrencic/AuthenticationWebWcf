using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using AuthenticationWebWcf.IService.Inspectors;
using AuthenticationWebWcf.Service.DataContracts;
using Ninject.Extensions.Logging;

namespace AuthenticationWebWcf.Service.Behaviors
{
    public class TokenValidationServiceBehaviorAttribute : Attribute, IServiceBehavior
    {
        private readonly ITokenDispatchMessageInspector tokenDispatchMessageInspector;
        private readonly IEnumerable<ILogger> loggers;

        public TokenValidationServiceBehaviorAttribute(ITokenDispatchMessageInspector tokenDispatchMessageInspector, IEnumerable<ILogger> loggers)
        {
            this.tokenDispatchMessageInspector = tokenDispatchMessageInspector;
            this.loggers = loggers;
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
                    foreach (var logger in loggers)
                    {
                        logger.Warn(msg);
                    }
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

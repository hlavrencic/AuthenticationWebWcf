using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace AuthenticationWebWcf.Service.Behaviors
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TokenValidatorOperationBehaviorAttribute : Attribute, IOperationBehavior
    {
        private readonly IList<string> permisos;

        public TokenValidatorOperationBehaviorAttribute(params string[] permisos)
        {
            this.permisos = permisos;
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public IEnumerable<string> GetPermisos()
        {
            return permisos ?? Enumerable.Empty<string>();
        } 
    }
}

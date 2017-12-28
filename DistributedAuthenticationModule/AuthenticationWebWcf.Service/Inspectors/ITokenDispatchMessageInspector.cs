using System.ServiceModel.Dispatcher;
using AuthenticationWebWcf.Common.Crypto;

namespace AuthenticationWebWcf.Service.Inspectors
{
    public interface ITokenDispatchMessageInspector : IDispatchMessageInspector, IJsonWebTokenSetKey
    {
    }
}
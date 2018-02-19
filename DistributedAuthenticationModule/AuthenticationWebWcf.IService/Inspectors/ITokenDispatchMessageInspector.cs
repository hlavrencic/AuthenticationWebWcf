using System.ServiceModel.Dispatcher;
using AuthenticationWebWcf.Common.Crypto;

namespace AuthenticationWebWcf.IService.Inspectors
{
    public interface ITokenDispatchMessageInspector : IDispatchMessageInspector, IJsonWebTokenSetKey
    {
    }
}
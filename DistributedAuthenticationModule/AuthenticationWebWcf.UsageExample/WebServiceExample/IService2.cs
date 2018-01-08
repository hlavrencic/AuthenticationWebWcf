using System.ServiceModel;

namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    [ServiceContract]
    public interface IService2
    {
        [OperationContract]
        void CallWebService();
    }
}
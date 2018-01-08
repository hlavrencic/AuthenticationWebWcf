using System.ServiceModel;

namespace AuthenticationWebWcf.UsageExample.WcfHost.WebServiceExample
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        SecuredData Method1();
    }
}

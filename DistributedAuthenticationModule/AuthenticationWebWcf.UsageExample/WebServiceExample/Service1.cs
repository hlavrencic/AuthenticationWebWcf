using System.ServiceModel;
using AuthenticationWebWcf.Service.ContextExtensions;

namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    public class Service1 : IService1
    {
        private readonly IAuthenticationDataExtensionReader<SecuredData> reader;

        public Service1(IAuthenticationDataExtensionReader<SecuredData> reader)
        {
            this.reader = reader;
        }

        #region Implementation of IService1

        public SecuredData Method1()
        {
            return reader.GetUser(OperationContext.Current);
        }

        #endregion
    }
}

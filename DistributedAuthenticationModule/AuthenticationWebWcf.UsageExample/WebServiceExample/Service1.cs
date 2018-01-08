using System;
using System.ServiceModel;
using AuthenticationWebWcf.Service.ContextExtensions;

namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    public class Service1 : IService1
    {
        private readonly IAuthenticationDataExtensionReader<SecuredData> reader;
        private readonly ILog logger;

        public Service1(
            IAuthenticationDataExtensionReader<SecuredData> reader,
            ILog logger)
        {
            this.reader = reader;
            this.logger = logger;
        }

        #region Implementation of IService1

        public void Method1()
        {
            var data = reader.GetUser(OperationContext.Current);
            logger.Log(data);
        }

        #endregion
    }
}

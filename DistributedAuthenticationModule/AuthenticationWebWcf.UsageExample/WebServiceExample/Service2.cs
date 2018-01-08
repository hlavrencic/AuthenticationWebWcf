namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    public class Service2 : IService2
    {
        private readonly IService1 service1;

        public Service2(
            IService1 service1)
        {
            this.service1 = service1;
        }

        #region Implementation of IService2

        public void CallWebService()
        {
            service1.Method1();
        }

        #endregion
    }
}
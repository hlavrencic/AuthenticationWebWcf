﻿using System.ServiceModel;

namespace AuthenticationWebWcf.UsageExample.WebServiceExample
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void Method1();
    }
}

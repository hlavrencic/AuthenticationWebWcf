using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AuthenticationWebWcf.Service.DataContracts
{
    [DataContract]
    [Serializable]
    public class UnauthorizedAccessFault
    {
        [DataMember]
        public IList<string> ErrorList { get; set; }
    }
}

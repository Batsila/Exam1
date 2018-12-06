using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerApp
{
    [ServiceContract]
    public interface ICommunicationService
    {
        [OperationContract]
        void DoWork();
    }
}

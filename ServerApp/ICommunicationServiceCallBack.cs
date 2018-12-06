using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerApp
{
    [ServiceContract]
    public interface ICommunicationServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void Disconnect(UserData user);

        [OperationContract(IsOneWay = true)]
        void Connect(UserData user);
    }
}

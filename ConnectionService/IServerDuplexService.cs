using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Shared
{
    [ServiceContract(CallbackContract = typeof(IClientDuplexCallback))]
    public interface IServerDuplexService
    {
        [OperationContract(IsOneWay = true)]
        void Connect();

        [OperationContract(IsOneWay = true)]
        void AddItem(DataItem item);

        [OperationContract(IsOneWay = true)]
        void Disconnect();
    }
}

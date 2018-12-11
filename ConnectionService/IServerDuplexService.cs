using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Shared
{
    /// <summary>
    /// Server duplex communication contract
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IClientDuplexCallback))]
    public interface IServerDuplexService
    {
        /// <summary>
        /// Determining connection method 
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Connect();

        /// <summary>
        /// Determining the method of adding a new user   
        /// </summary>
        /// <param name="item"></param>
        [OperationContract(IsOneWay = true)]
        void AddItem(DataItem item);

        /// <summary>
        /// Determining how to disconnect a new user
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void Disconnect();
    }
}

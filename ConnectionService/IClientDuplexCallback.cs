using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    /// <summary>
    /// Definition of response interface for duplex service
    /// </summary>
    public interface IClientDuplexCallback
    {
        /// <summary>
        /// Determine the method of deletion notification
        /// </summary>
        [OperationContract(IsOneWay = true)]
        void NotifyItemDeleted(DataItem item);
    }
}

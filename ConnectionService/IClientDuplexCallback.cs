using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public interface IClientDuplexCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyItemDeleted(DataItem item);
    }
}

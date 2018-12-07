using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Managers
{
    public class CommunicationManager
    {
        public void ItemDeleted(ClientModel deletedItem)
        {

        }

        public event EventHandler<ClientModel> ItemAdding;
    }
}

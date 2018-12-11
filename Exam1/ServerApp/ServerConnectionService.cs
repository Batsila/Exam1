using ConnectionService;
using DataGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServerApp
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServerConnectionService" in both code and config file together.
    public class ServerConnectionService : IServerConnectionService
    {
        public void AddUser(string firstName, string lastName)
        {
            //TODO: add user to dataGrid
        }
    }
}

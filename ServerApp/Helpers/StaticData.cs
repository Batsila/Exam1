using ServerApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Helpers
{
    static class StaticData
    {
        public static IEnumerable<ClientModel> GetData()
        {
            return new List<ClientModel>
            {
                new ClientModel { Id = 1, IsActive = true,  Address = "192.168.0.1", Model = "K2GHJK", Vendor = "Siemens" },
                new ClientModel { Id = 2, IsActive = true,  Address = "192.168.0.3", Model = "K2343456F", Vendor = "D-LINK" },
                new ClientModel { Id = 3, IsActive = false, Address = "192.168.0.19", Model = "N-3784G", Vendor = "Axis" },
                new ClientModel { Id = 4, IsActive = true,  Address = "192.168.0.122", Model = "G4BF" },
                new ClientModel { Id = 5, IsActive = false, Address = "192.168.0.14", Model = "BCVHJDFGGGHJE43", Vendor = "ZTE" },
                new ClientModel { Id = 6, IsActive = true,  Address = "192.168.0.230", Model = "345NJHDFG", Vendor = "Siemens" },
                new ClientModel { Id = 7, IsActive = true,  Address = "192.168.0.2", Model = "123JFGJH" }
            };
        }
    }
}

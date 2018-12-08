using Shared;
using System.ServiceModel;

namespace ClientApp.Service
{
    internal class DuplexServiceClient : DuplexClientBase<IServerDuplexService>, IServerDuplexService
    {
        public DuplexServiceClient(InstanceContext callbackInstance, string configName)
            : base(callbackInstance, configName) { }

        public void Connect()
        {
            Channel.Connect();
        }

       public void Disconnect()
        {
            Channel.Disconnect();
        }

        public void AddItem(DataItem item)
        {
            Channel.AddItem(item);
        }
    }
}

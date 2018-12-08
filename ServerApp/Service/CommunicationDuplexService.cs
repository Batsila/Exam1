using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ServerApp.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class CommunicationDuplexService : IServerDuplexService
    {
        private static readonly object _sync = new object();
        private static List<IClientDuplexCallback> _callbackChannels = new List<IClientDuplexCallback>();

        public CommunicationDuplexService()
        {
        }

        public event EventHandler<DataItem> ItemAddRequest;

        public void AddItem(DataItem item)
        {
            ItemAddRequest?.Invoke(this, item);
        }

        public void RemoveItem(DataItem item)
        {
            lock (_sync)
            {
                for (int i = 0; i < _callbackChannels.Count; i++)
                {
                    if (((ICommunicationObject)_callbackChannels[i]).State != CommunicationState.Opened)
                    {
                        _callbackChannels.RemoveAt(i);
                        continue;
                    }

                    try
                    {
                        _callbackChannels[i].NotifyItemDeleted(item);
                    }
                    catch
                    {
                        _callbackChannels.RemoveAt(i);
                    }
                }
            }
        }

        public void Connect()
        {
            try
            {
                IClientDuplexCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IClientDuplexCallback>();

                lock (_sync)
                {
                    if (!_callbackChannels.Contains(callbackChannel))
                    {
                        _callbackChannels.Add(callbackChannel);
                    }
                }
            }
            catch
            {
            }
        }

        public void Disconnect()
        {
            IClientDuplexCallback callbackChannel = OperationContext.Current.GetCallbackChannel<IClientDuplexCallback>();

            try
            {
                lock (_sync)
                {
                    _callbackChannels.Remove(callbackChannel);
                }
            }
            catch
            {
            }
        }
    }
}

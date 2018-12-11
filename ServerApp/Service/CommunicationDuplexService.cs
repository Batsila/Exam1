using Shared;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServerApp.Service
{
    /// <summary>
    /// The implementation of the duplex service interface.
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    internal class CommunicationDuplexService : IServerDuplexService
    {
        private static readonly object _sync = new object();
        private static List<IClientDuplexCallback> _callbackChannels = new List<IClientDuplexCallback>();

        public CommunicationDuplexService()
        {
        }

        public event EventHandler<DataItem> ItemAddRequest;


        /// <summary>
        /// Service methods that handles item add requests from clients
        /// </summary>
        public void AddItem(DataItem item)
        {
            ItemAddRequest?.Invoke(this, item);
        }

        /// <summary>
        /// Notify all connected clients that item was removed
        /// </summary>
        public void RemoveItem(DataItem item)
        {
            lock (_sync)
            {
                for (int i = 0; i < _callbackChannels.Count; i++)
                {
                    if (((ICommunicationObject) _callbackChannels[i]).State != CommunicationState.Opened)
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

        /// <summary>
        /// Client connection handler
        /// </summary>
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

        /// <summary>
        /// Client disconnect handler
        /// </summary>
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

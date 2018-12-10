using Shared;
using System;
using System.ServiceModel;
using System.Windows;

namespace ClientApp.Service
{
    [CallbackBehavior(UseSynchronizationContext = false)]
    internal class DuplexCallback : IClientDuplexCallback
    {
        public event EventHandler<DataItem> ItemDeleted;

        public void NotifyItemDeleted(DataItem item)
        {
            Application.Current.Dispatcher.InvokeAsync(() => {

                ItemDeleted?.Invoke(this, item);

            });
        }
    }
}

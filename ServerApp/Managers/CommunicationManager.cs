﻿using ServerApp.Model;
using ServerApp.Service;
using System;
using System.Configuration;
using System.ServiceModel;

namespace ServerApp.Managers
{
    public class CommunicationManager : IDisposable
    {
        private readonly CommunicationDuplexService _comService;
        private readonly ServiceHost _serviceHost;

        public CommunicationManager()
        {
            _comService = new CommunicationDuplexService();
            _comService.ItemAddRequest += ServiceItemAddRequest;

            // TODO: move to config file  //DONE
            //var baseAddress = new Uri("net.tcp://localhost:9000/Service");
            _serviceHost = new ServiceHost(_comService);

            _serviceHost.Open();
        }

        private void ServiceItemAddRequest(object sender, Shared.DataItem e)
        {
            if (e == null)
                return;

            ItemAdding?.Invoke(this, new ClientModel
            {
                Address = e.Address,
                Id = e.Id,
                IsActive = e.IsOnline,
                Model = e.Model,
                Vendor = e.Vendor
            });
        }

        public void ItemDeleted(ClientModel deletedItem)
        {
            if (deletedItem == null)
                throw new ArgumentNullException(nameof(deletedItem));

            _comService.RemoveItem(new Shared.DataItem
            {
                Id = deletedItem.Id,
                Address = deletedItem.Address,
                IsOnline = deletedItem.IsActive,
                Model = deletedItem.Model,
                Vendor = deletedItem.Vendor
            });
        }


        public event EventHandler<ClientModel> ItemAdding;



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _serviceHost.Close();
            }
        }
    }
}

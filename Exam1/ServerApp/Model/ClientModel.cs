﻿using Prism.Mvvm;
namespace ServerApp.Model
{
    /// <summary>
    /// This is the class of the model that describes the information written to the table.
    /// </summary>
    public class ClientModel : BindableBase
    {
        private int _id;
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _vendor;
        public string Vendor
        {
            get { return _vendor; }
            set { SetProperty(ref _vendor, value); }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set { SetProperty(ref _isActive, value); }
        }

    }
}

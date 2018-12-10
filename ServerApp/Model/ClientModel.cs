using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ServerApp.Model
{
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

        private string _status;

        public string Status
        {
            get {
                if (_isActive)
                {
                    return _status = "D:\\Job\\Exam1\\ServerApp\\Properties\\online.png";

                }
                else
                {
                    return _status = "D:\\Job\\Exam1\\ServerApp\\Properties\\offline.png";
                }
            }
        }

    }
}

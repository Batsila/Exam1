using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ServerApp;

namespace ClientApp
{
    class ClientServise : ICommunicationServiceCallBack
    {
        public void Connect(UserData user)
        {
            MessageBox.Show("Connect new user");
        }

        public void Disconnect(UserData user)
        {
            MessageBox.Show("Disconnect this user");
        }
    }
}

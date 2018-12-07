using System.Runtime.Remoting.Proxies;
using ServerApp;
using System.Windows;

namespace ClientApp
{
    class ClientServise
    {
        public void Connect()
        {
            MessageBox.Show("Connect new user");
        }

        public void Disconnect()
        {
            MessageBox.Show("User is disconnect");
        }
    }
}

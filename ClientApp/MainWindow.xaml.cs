using System;
using ClientApp.Service;
using System.ComponentModel;
using System.ServiceModel;
using System.Windows;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DuplexServiceClient _proxy;

        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
        }

        /// <summary>
        /// Handling a send event to the server with a connection check
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnSendClick(object sender, RoutedEventArgs e)
        {
            InitializeClient();
            try
            {
                Random rnd = new Random();
                _proxy.AddItem(new Shared.DataItem
                {

                    Address = tbAddress.Text,
                    Id = rnd.Next(),
                    IsOnline = cbIsActive.IsChecked ?? false,
                    Model = tbModel.Text,
                    Vendor = tbVendor.Text

                });
            }
            catch
            {
                MessageBox.Show("Server is not active");
            }
            
        }

        /// <summary>
        /// Creating a connection and displaying server status
        /// </summary>
        private void InitializeClient()
        {
            if (_proxy != null)
            {
                try
                {
                    _proxy.Close();
                }
                catch
                {
                    _proxy.Abort();
                }
                finally
                {
                    _proxy = null;
                }
            }

            var duplexCallback = new DuplexCallback();
            duplexCallback.ItemDeleted += CallbackItemDeleted;

            var instanceContext = new InstanceContext(duplexCallback);
            _proxy = new DuplexServiceClient(instanceContext, "duplexClient");
            try
            {
                _proxy.Open();
                _proxy.Connect();
                lbServerStatus.Content = "On";
            }
            catch
            {
                lbServerStatus.Content = "Off";
            }
        }

        /// <summary>
        /// Display information of a disabled user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CallbackItemDeleted(object sender, Shared.DataItem e)
        {
            MessageBox.Show($"Item with id {e.Id} was deleted\nIP adress:   {e.Address}\nModel:   {e.Model}\nVendor:   {e.Address}");
        }

        /// <summary>
        /// Closing connection
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (_proxy != null)
            {
                try
                {
                    _proxy.Disconnect();
                    _proxy.Close();
                }
                catch
                {
                    _proxy.Abort();
                }
            }

            base.OnClosing(e);
        }
    }
}

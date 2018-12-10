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
        // TODO: Shall have an indication to Online / Offline connection.


        private DuplexServiceClient _proxy;

        public MainWindow()
        {
            InitializeComponent();
            InitializeClient();
        }

        public void BtnSendClick(object sender, RoutedEventArgs e)
        {
            // TODO: Add exception handling and status change
            Random rnd = new Random();
            _proxy.AddItem(new Shared.DataItem
            {

                Address = tbAddress.Text,
                Id = rnd.Next(),
                IsOnline = cbIsActive.IsChecked ?? false,
                Model = tbModel.Text,
                Vendor = tbVendor.Text

            });

            lbConnectStatus.Content = "Online";
            lbConnectStatus.Foreground = System.Windows.Media.Brushes.Green;
        }


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
                btnSend.IsEnabled = false;
                lbServerStatus.Content = "Off";
            }
        }

        private void CallbackItemDeleted(object sender, Shared.DataItem e)
        {
            // TODO: Detailed information
            MessageBox.Show($"Item with id {e.Id} was deleted");
            lbConnectStatus.Content = "Offline";
            lbConnectStatus.Foreground = System.Windows.Media.Brushes.Red;
        }

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

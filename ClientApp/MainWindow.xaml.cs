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

            _proxy.AddItem(new Shared.DataItem {

                Address = tbAddress.Text,
                Id = 12,
                IsOnline = cbIsActive.IsChecked ?? false,
                Model = tbModel.Text,
                Vendor = tbVendor.Text

            });
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
            _proxy.Open();
            _proxy.Connect();
        }

        private void CallbackItemDeleted(object sender, Shared.DataItem e)
        {
            // TODO: Detailed information
            MessageBox.Show($"Item with id {e.Id} was deleted");
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

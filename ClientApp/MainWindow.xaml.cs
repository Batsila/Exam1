using ServerApp;
using System.ServiceModel;
using System.Windows;
namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [CallbackBehavior(UseSynchronizationContext = false)]
    public partial class MainWindow : Window
    { 
        private readonly ClientServise _clientServise;

        public MainWindow()
        {
            InitializeComponent();
            _clientServise = new ClientServise();
            
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            /*MessageBox.Show("First Name: " + FirstNameInput.Text 
                + "\n Last Name: " + LastNameInput.Text);*/
            /*using (var host = new ServiceHost(typeof(CommunicationService)))
            {
                host.Open();
                //InstanceContext instanceContext = new InstanceContext(new ClientServise());
                

            }*/
            CommunicationService service = new CommunicationService();
            service.Connect(new UserData(1, FirstNameInput.Text, LastNameInput.Text));

        }
    }
}

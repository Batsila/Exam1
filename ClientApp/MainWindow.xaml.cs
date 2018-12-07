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

        public MainWindow()
        {
            InitializeComponent();
            
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("First Name: " + FirstNameInput.Text 
                + "\n Last Name: " + LastNameInput.Text);

            var client = new ServerConnectionService.ServerConnectionServiceClient();

            client.AddUser(FirstNameInput.Text, LastNameInput.Text);

        }
    }
}

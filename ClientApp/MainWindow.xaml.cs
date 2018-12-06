using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServerApp;

namespace ClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClientServise clientServise;
        public MainWindow()
        {
            InitializeComponent();
            clientServise = new ClientServise();
            using (var host = new ServiceHost(typeof(CommunicationService)))
            {
                host.Open();
                
            }
        }

        public void Send(object sender, RoutedEventArgs e)
        {
            /*MessageBox.Show("First Name: " + FirstNameInput.Text 
                + "\n Last Name: " + LastNameInput.Text);*/
                clientServise.Connect(new UserData(1, FirstNameInput.Text, LastNameInput.Text));
        }
    }
}

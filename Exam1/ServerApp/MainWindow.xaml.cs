using System;
using ServerApp;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ServiceHost _host;
        public List<UserData> _users { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            _users = new List<UserData>
            {
                new UserData
                {
                    IP = GetIP(),
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                },
                new UserData
                {
                    IP = GetIP(),
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                },
                new UserData
                {
                    IP = GetIP(),
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                }
            };
            grid.ItemsSource =  _users;
            _host = new ServiceHost(typeof(ServerConnectionService));
            _host.Open();
        }

        private void GridMouseUp(object sender, MouseButtonEventArgs e)
        {
            var userData = grid.SelectedItem as UserData;
            if (userData != null)
            {
                MessageBox.Show("IP: " + userData.IP + "\nStatus: " + userData.Online
                    + "\nFirst Name: " + userData.FirstName + "\nLast Name: " + userData.LastName);
            }
        }

        public void AddUser(string firstName, string lastName)
        {
            var userData = new UserData
            {
                IP = GetIP(),
                FirstName = firstName,
                LastName = lastName,
                Online = true
            };
            _users.Add(userData);
            grid.Items.Refresh();
            var row = grid.ItemContainerGenerator.ContainerFromItem(userData);
        }

        public string GetIP()
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            return myIP;
        }
    }

}
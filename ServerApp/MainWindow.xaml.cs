using System;
using ServerApp;
using System.Collections.Generic;
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
                    Id = 1,
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                },
                new UserData
                {
                    Id = 2,
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                },
                new UserData
                {
                    Id = 3,
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
                MessageBox.Show("ID: " + userData.Id + "\n Status: " + userData.Online
                    + "\n First Name: " + userData.FirstName + "\n Last Name: " + userData.LastName);
            }
        }

        public void AddUser(string firstName, string lastName)
        {
            var userData = new UserData
            {
                Id = _users.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                Online = true
            };
            _users.Add(userData);
            grid.Items.Refresh();
            var row = grid.ItemContainerGenerator.ContainerFromItem(userData);
        }
    }

}
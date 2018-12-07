using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.ViewModel
{
    public class MainGridViewModel : BindableBase
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }



        //private ServiceHost _host;
        public List<UserData> _users { get; private set; }

        public MainGridViewModel()
        {
            _users = new List<UserData>
            {
                new UserData
                {
                    //IP = 1,
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                },
                new UserData
                {
                    //IP = 2,
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                },
                new UserData
                {
                    //IP = 3,
                    FirstName = "test",
                    LastName = "test",
                    Online = true
                }
            };
            //grid.ItemsSource = _users;
            //_host = new ServiceHost(typeof(ServerConnectionService));
            //_host.Open();
        }

        //private void GridMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    var userData = grid.SelectedItem as UserData;
        //    if (userData != null)
        //    {
        //        MessageBox.Show("ID: " + userData.Id + "\n Status: " + userData.Online
        //            + "\n First Name: " + userData.FirstName + "\n Last Name: " + userData.LastName);
        //    }
        //}

        public void AddUser(string firstName, string lastName)
        {
            var userData = new UserData
            {
                //IP = _users.Count + 1,
                FirstName = firstName,
                LastName = lastName,
                Online = true
            };
            _users.Add(userData);
            //grid.Items.Refresh();
            //var row = grid.ItemContainerGenerator.ContainerFromItem(userData);
        }
    }
}

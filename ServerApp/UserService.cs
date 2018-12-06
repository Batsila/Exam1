using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerApp
{
    class UserService
    {
        private static List<UserData> userlist = new List<UserData>();

        public UserService()
        {
            
        }

        public List<UserData> GetUserList()
        {
            return userlist;
        }

        public void AddNewItem(UserData user)
        {
            userlist.Add(user);
            MessageBox.Show("Server: Connect new user");
        }

        public void RemoveItem(UserData user)
        {
            userlist.Remove(user);
        }

        public void Disconnect(UserData user)
        {
            foreach (var item in userlist)
            {
                if (item == user)
                {
                    item.Online = false;
                }
            } 
        }
    }
}

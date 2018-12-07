using System;
using ServerApp;
using System.Collections.Generic;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ServiceHost serviceHost;
        private UserService userService;
        public MainWindow()
        {
            InitializeComponent();
            userService = new UserService();
            userService.AddNewItem(new UserData(1, "Yura", "Varlakov"));
        }

        //Загрузка содержимого таблицы
        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            grid.ItemsSource = userService.GetUserList();
        }

        //Получаем данные из таблицы
        private void GridMouseUp(object sender, MouseButtonEventArgs e)
        {
            UserData path = grid.SelectedItem as UserData;
            MessageBox.Show("ID: " + path.Id + "\n Status: " + path.Online + "\n First Name: " + path.FirstName + "\n Last Name: " + path.LastName);
            MessageBox.Show("Всего записей: " + userService.GetUserList().Count);
        }
    }

}
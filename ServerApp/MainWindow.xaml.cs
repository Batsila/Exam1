using ServerApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        //Загрузка содержимого таблицы
        private void GridLoaded(object sender, RoutedEventArgs e)
        {
            List<UserData> result = new List<UserData>(3)
            {
                new UserData(1, "Yura", "Varlakov"),
                new UserData(2, "NotYura", "Varlakov"),
                new UserData(3, "Yura", "NotVarlakov"),
                new UserData(4, "NotYura", "NotVarlakov")
            };
            grid.ItemsSource = result;
        }

        //Получаем данные из таблицы
        private void GridMouseUp(object sender, MouseButtonEventArgs e)
        {
            UserData path = grid.SelectedItem as UserData;
            MessageBox.Show(" ID: " + path.Id + "\n First Name: " + path.FirstName + "\n Last Name: " + path.LastName);
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
        }


        private void Configuration_Click(object sender, RoutedEventArgs e)
        {
            Top1.Visibility = Visibility.Visible;
            Top2.Visibility = Visibility.Visible;
            Top1.Content = "Create User";
            Top2.Content = "Something else";
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            Top1.Visibility = Visibility.Visible;
            Top2.Visibility = Visibility.Visible;
            Top3.Visibility = Visibility.Visible;
            Top1.Content = "Load";
            Top2.Content = "Update";
            Top3.Content = "Something";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

    }
}

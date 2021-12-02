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

        private void LogFiles_Click(object sender, RoutedEventArgs e)
        {
            Disable_Menu();
            Disable_Lists();
            Disable_Buttons();
            LogFiles.Background = Brushes.LightSkyBlue;
        }

        private void Configuration_Click(object sender, RoutedEventArgs e)
        {
            Disable_Menu();
            Disable_Lists();
            Disable_Buttons();
            Configuration.Background = Brushes.LightSkyBlue;
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            Disable_Menu();
            Disable_Lists();
            Disable_Buttons();
            Database.Background = Brushes.LightSkyBlue;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void Disable_Menu()
        {
            LogFiles.Background = Brushes.WhiteSmoke;
            Database.Background = Brushes.WhiteSmoke;
            Configuration.Background = Brushes.WhiteSmoke;
                       
        }

        private void Disable_Lists()
        {
            //ContractsList.Visibility = Visibility.Hidden;
            //InvoicesList.Visibility = Visibility.Hidden;
            //CarriersList.Visibility = Visibility.Hidden;
            //OrdersList.Visibility = Visibility.Hidden;
            //ClientsList.Visibility = Visibility.Hidden;
        }

        private void Disable_Buttons()
        {
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;
        }
        
    }
}

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
    /// Interaction logic for PlannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Window
    {
        public PlannerPage()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            Disable_Buttons();
            Disable_Lists();
            Disable_Menu();

            Reports.Background = Brushes.LightSkyBlue;
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            Disable_Buttons();
            Disable_Lists();
            Disable_Menu();

            Activities.Background = Brushes.LightSkyBlue;
        }

        private void Trip_Click(object sender, RoutedEventArgs e)
        {
            Disable_Buttons();
            Disable_Lists();
            Disable_Menu();

            Trip.Background = Brushes.LightSkyBlue;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Disable_Buttons();
            Disable_Lists();
            Disable_Menu();

            Orders.Background = Brushes.LightSkyBlue;
        }

        private void Disable_Menu()
        {
            Reports.Background = Brushes.WhiteSmoke;
            Activities.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;
            Trip.Background = Brushes.WhiteSmoke;
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

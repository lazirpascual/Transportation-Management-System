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
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Window
    {
        public BuyerPage()
        {
            InitializeComponent();
            MarketPlace_Page();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void MarketPlace_Click(object sender, RoutedEventArgs e)
        {
            Disable_Lists();
            Disable_Menu();
            Disable_Buttons();
            MarketPlace_Page();
        }
        
        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            Disable_Lists();
            Disable_Menu();
            Disable_Buttons();
            Invoice.Background = Brushes.LightSkyBlue;
        }

        private void Carriers_Click(object sender, RoutedEventArgs e)
        {
            Disable_Lists();
            Disable_Menu();
            Disable_Buttons();
            Carriers.Background = Brushes.LightSkyBlue;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Disable_Lists();
            Disable_Menu();
            Disable_Buttons();
            Orders.Background = Brushes.LightSkyBlue;

            //Buyer newBuyer = new Buyer(); 
            List<Order> orderList = new List<Order>();
                        
            OrdersList.ItemsSource = orderList;
            OrdersList.Visibility = Visibility.Visible;
            ActiveBox.Visibility = Visibility.Visible;


        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            Disable_Lists();
            Disable_Menu();
            Disable_Buttons();
            Clients.Background = Brushes.LightSkyBlue;
        }

        private void MarketPlace_Page()
        {
            ContractMarketPlace CMP = new ContractMarketPlace();
            List<Contract> contractList = new List<Contract>();
            contractList = CMP.GetContracts();
            ContractsList.ItemsSource = contractList;
            ContractsList.Visibility = Visibility.Visible;
            Button5.Visibility = Visibility.Visible;
            Button5.Content = "Accept Contract(s)";
            MarketPlace.Background = Brushes.LightSkyBlue;
        }

        private void ActiveBox_Checked(object sender, RoutedEventArgs e)
        {
            if(ActiveBox.IsChecked == true)
            {
                //show only active
            }
            else
            {
                //show all
            }
        }


        private void Disable_Menu()
        {
            MarketPlace.Background = Brushes.WhiteSmoke;
            Clients.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;
            Carriers.Background = Brushes.WhiteSmoke;
            Invoice.Background = Brushes.WhiteSmoke;
        }

        private void Disable_Lists()
        {
            ContractsList.Visibility = Visibility.Hidden;
            InvoicesList.Visibility = Visibility.Hidden;
            CarriersList.Visibility = Visibility.Hidden;
            OrdersList.Visibility = Visibility.Hidden;
            ClientsList.Visibility = Visibility.Hidden;
        }

        private void Disable_Buttons()
        {
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;
            ActiveBox.Visibility = Visibility.Hidden;
        }
                
    }
}

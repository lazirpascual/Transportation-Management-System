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
        private List<Contract> contractList;
        private Buyer buyer;

        public BuyerPage()
        {
            InitializeComponent();
            buyer = new Buyer();
            MarketPlace_Page();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void MarketPlace_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            MarketPlace_Page();
        }
        
        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Invoice.Background = Brushes.LightSkyBlue;
        }

        private void Carriers_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Carriers.Background = Brushes.LightSkyBlue;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Orders.Background = Brushes.LightSkyBlue;
                         
            List<Order> orderList = new List<Order>();
            orderList = buyer.GetOrders(false);             
            OrdersList.ItemsSource = orderList;
            OrdersList.Visibility = Visibility.Visible;
            ActiveBox.Visibility = Visibility.Visible;

        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Clients.Background = Brushes.LightSkyBlue;
        }

        private void MarketPlace_Page()
        {
            resetStatus();
            ContractMarketPlace CMP = new ContractMarketPlace();
            contractList = new List<Contract>();
            contractList = CMP.GetContracts();
            ContractsList.ItemsSource = contractList;
            ContractsList.Visibility = Visibility.Visible;
            AcceptClient.Visibility = Visibility.Visible;
            MarketPlace.Background = Brushes.LightSkyBlue;
        }

        private void ActiveBox_Click(object sender, RoutedEventArgs e)
        {
            var orderList = new List<Order>();
            Buyer buyer = new Buyer();
            // Only active orders
            if(ActiveBox.IsChecked == true)
            {
                orderList = buyer.GetOrders(true);
                
            }
            // Show all orders
            else
            {
                orderList = buyer.GetOrders();
            }

            OrdersList.ItemsSource = orderList;
        }
              
        private void resetStatus()
        {
            // reset buttons status
            AcceptClient.Visibility = Visibility.Hidden;
            ActiveBox.Visibility = Visibility.Hidden;

            // reset previous lists
            ContractsList.Visibility = Visibility.Hidden;
            InvoicesList.Visibility = Visibility.Hidden;
            CarriersList.Visibility = Visibility.Hidden;
            OrdersList.Visibility = Visibility.Hidden;
            ClientsList.Visibility = Visibility.Hidden;

            // reset menu buttons to non-clicked status
            MarketPlace.Background = Brushes.WhiteSmoke;
            Clients.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;
            Carriers.Background = Brushes.WhiteSmoke;
            Invoice.Background = Brushes.WhiteSmoke;
        }

        private void AcceptClient_Click(object sender, RoutedEventArgs e)
        {
            // Get the current contract list shown in the table
            var currentList = ContractsList.ItemsSource.Cast<Contract>().ToList();

            Buyer buyer = new Buyer();

            // Get all the contracts selected and generate order for them and remove from the list
            foreach (var contract in ContractsList.SelectedItems)
            {
                buyer.GenerateOrder((Contract)contract);
                currentList.Remove((Contract)contract);
            }

            // Update the contracts list
            ContractsList.ItemsSource = currentList;
        }
    }
}

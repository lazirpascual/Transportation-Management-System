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
            InvoicesGrid.Visibility = Visibility.Visible;
            Invoice.Background = Brushes.LightSkyBlue;

            List<Order> orderList = new List<Order>();
            orderList = buyer.GetOrders(1);
            InvoiceList.ItemsSource = orderList;
        }

        private void Carriers_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            CarriersGrid.Visibility = Visibility.Visible;
            Carriers.Background = Brushes.LightSkyBlue;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();

            ActiveBox.IsChecked = false;
            CompletedBox.IsChecked = false;
            OrdersGrid.Visibility = Visibility.Visible;
            Orders.Background = Brushes.LightSkyBlue;
            List<Order> orderList = new List<Order>();
            orderList = buyer.GetOrders(25);             
            OrdersList.ItemsSource = orderList;
            ActiveBox.IsChecked = false;
            CompletedBox.IsChecked = false;
            GenerateInvoice.Visibility = Visibility.Hidden;
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            ClientsGrid.Visibility = Visibility.Visible;
            Clients.Background = Brushes.LightSkyBlue;

            List<Client> clientList = new List<Client>();

            clientList = buyer.FetchClients(25);
            ClientsList.ItemsSource = clientList;
        }

        private void MarketPlace_Page()
        {
            resetStatus();
            ContractMarketPlace CMP = new ContractMarketPlace();
            MarketPlaceGrid.Visibility = Visibility.Visible;
            contractList = new List<Contract>();
            contractList = CMP.GetContracts();
            ContractsList.ItemsSource = contractList;
            MarketPlace.Background = Brushes.LightSkyBlue;
        }

        private void ActiveBox_Click(object sender, RoutedEventArgs e)
        {
            var orderList = new List<Order>();
            GenerateInvoice.Visibility = Visibility.Hidden;
            if (ActiveBox.IsChecked == true)
            {
                // active box is checked
                if (CompletedBox.IsChecked == true)
                {
                    // completed box is also checked, get all orders
                    orderList = buyer.GetOrders(25);

                }
                else
                {
                    // completed box is not checked, fetch only the active orders
                    orderList = buyer.GetOrders(0);
                }
            }
            else
            {
                // active box is not checked
                if (CompletedBox.IsChecked == true)
                {
                    // completed box is checked, fetch only completed orders
                    orderList = buyer.GetOrders(1);
                }
                else
                {
                    // completed box is not checked, fetch all orders
                    orderList = buyer.GetOrders(25);
                }
            }

            OrdersList.ItemsSource = orderList;
        }

        private void CompletedBox_Click(object sender, RoutedEventArgs e)
        {
            var orderList = new List<Order>();

            if (CompletedBox.IsChecked == true)
            {
                // completed box is checked
                if (ActiveBox.IsChecked == true)
                {
                    // active box is also checked, get all orders
                    orderList = buyer.GetOrders(25);
                }
                else
                {
                    // active box is not checked, get only completed orders
                    orderList = buyer.GetOrders(1);
                }
            }
            else
            {
                GenerateInvoice.Visibility = Visibility.Hidden;
                // completed box is not checked
                if (ActiveBox.IsChecked == true)
                {
                    // active box is checked, fetch only active orders
                    orderList = buyer.GetOrders(0);
                }
                else
                {
                    // active box is also not checked, fetch all orders
                    orderList = buyer.GetOrders(25);
                }
            }

            OrdersList.ItemsSource = orderList;
        }

        private void resetStatus()
        {
            // Hide all modules
            ClientsGrid.Visibility = Visibility.Hidden;
            OrdersGrid.Visibility = Visibility.Hidden;
            MarketPlaceGrid.Visibility = Visibility.Hidden;
            CarriersGrid.Visibility = Visibility.Hidden;
            InvoicesGrid.Visibility = Visibility.Hidden;

            // Reset menu buttons to non-clicked status
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

        private void CActiveBox_Click(object sender, RoutedEventArgs e)
        {
            var clientList = new List<Client>();

            if (CActiveBox.IsChecked == true)
            {
                // active box is checked
                clientList = buyer.FetchClients(0);
            }
            else
            {
                clientList = buyer.FetchClients(1);
            }
            ClientsList.ItemsSource = clientList;
        }

        //private void OrdersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Order currentOrder = (Order)OrdersList.SelectedItem;
        //    if (currentOrder != null)
        //    {
        //        if (buyer.InvoiceGeneration(currentOrder) == false)
        //        {
        //            GenerateInvoice.Visibility = Visibility.Visible;                  
        //        }
        //        else
        //        {
        //            GenerateInvoice.Visibility = Visibility.Hidden;
        //        }
        //    }
        //}

        private void InvoiceList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GenerateInvoice.Visibility = Visibility.Visible;
        }

        private void GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

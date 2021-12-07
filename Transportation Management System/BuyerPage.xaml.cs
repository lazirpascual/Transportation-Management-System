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
        private readonly Buyer buyer = new Buyer();

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
            ResetStatus();
            MarketPlace_Page();
        }
        
        private void Invoice_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            InvoicesGrid.Visibility = Visibility.Visible;
            Invoice.Background = Brushes.LightSkyBlue;

            List<Order> orderList = buyer.GetOrders(1);
            InvoiceList.ItemsSource = orderList;
        }



        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();

            AllBox.IsChecked = true;

            Orders.Background = Brushes.LightSkyBlue;
            OrdersGrid.Visibility = Visibility.Visible;

            Refresh_Orders();

        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            ClientsGrid.Visibility = Visibility.Visible;
            Clients.Background = Brushes.LightSkyBlue;

            List<Client> clientList = buyer.FetchClients(25);
            ClientsList.ItemsSource = clientList;
            CActiveBox.IsChecked = false;
        }

        private void MarketPlace_Page()
        {
            ResetStatus();
            ContractMarketPlace CMP = new ContractMarketPlace();
            MarketPlaceGrid.Visibility = Visibility.Visible;
            contractList = new List<Contract>();
            contractList = CMP.GetContracts();
            ContractsList.ItemsSource = contractList;
            MarketPlace.Background = Brushes.LightSkyBlue;
        }

        private void Refresh_Orders()
        {
            var orderList = new List<Order>();

            if (AllBox.IsChecked == true)
            {
                // Get all orders
                orderList = buyer.GetOrders(2);
            }
            else if (ActiveBox.IsChecked == true)
            {
                // Get active orders
                orderList = buyer.GetOrders(0);
            }
            else if (CompletedBox.IsChecked == true)
            {
                // completed box is checked, fetch only completed orders
                orderList = buyer.GetOrders(1);
            }

            OrdersList.ItemsSource = orderList;
        }

        private void AllBox_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Orders();
        }

        private void ActiveBox_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Orders();
        }

        private void CompletedBox_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Orders();
        }

        private void ResetStatus()
        {
            // Hide all modules
            ClientsGrid.Visibility = Visibility.Hidden;
            OrdersGrid.Visibility = Visibility.Hidden;
            MarketPlaceGrid.Visibility = Visibility.Hidden;
            CarriersGrid.Visibility = Visibility.Hidden;
            InvoicesGrid.Visibility = Visibility.Hidden;
            GenerateInvoice.Visibility = Visibility.Hidden;

            // Reset menu buttons to non-clicked status
            MarketPlace.Background = Brushes.WhiteSmoke;
            Clients.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;
            Invoice.Background = Brushes.WhiteSmoke;

            AllBox.IsChecked = false;
            ActiveBox.IsChecked = false;
            CompletedBox.IsChecked = false;
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
            List<Client> clientList;

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
            Order selectedInvoice = (Order)InvoiceList.SelectedItem;
            Invoice invoice = buyer.CreateInvoice(selectedInvoice);
            InvoiceInformation inv = new InvoiceInformation(invoice);
            inv.ShowDialog();
        }

    }
}

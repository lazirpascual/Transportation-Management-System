/* -- FILEHEADER COMMENT --
    FILE		:	BuyerPage.xaml.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Buyer user's user interface when they log in.
                    It allows a buyer to perform all their functionalities such as; accepting an order,
                    generating invoice for completed orders, viewing the list of clients etc.
*/

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Window
    {
        // list of contacts
        private List<Contract> contractList;

        // Buyer object to access the Buyer class.
        private readonly Buyer buyer = new Buyer();

        ///
        /// \brief This constructor is used to initialize the visibility status of components within the Buyer UI.
        ///
        public BuyerPage()
        {
            InitializeComponent();
            MarketPlace_Page();
        }

        ///
        /// \brief Event handler for when window is closed.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>System.ComponentModel.CancelEventArgs</b> - base class used to pass data to cancelable event.
        ///
        /// \return None - void
        ///
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        ///
        /// \brief Event handler for when MarketPlace button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void MarketPlace_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            MarketPlace_Page();
        }

        ///
        /// \brief Event handler for when an order is selected in the list.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void Selection_Changed(object sender, RoutedEventArgs e)
        {
            // order selection has changed
            Order currentOrder = (Order)OrdersList.SelectedItem;
            if (currentOrder != null && currentOrder.IsCompleted == 1)
            {
                GenerateInvoice.Visibility = Visibility.Visible;
            }
            else
            {
                GenerateInvoice.Visibility = Visibility.Hidden;
            }
        }

        ///
        /// \brief Event handler for when Orders button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();

            AllBox.IsChecked = true;

            Orders.Background = Brushes.LightSkyBlue;
            OrdersGrid.Visibility = Visibility.Visible;

            Refresh_Orders();
        }

        ///
        /// \brief Event handler for when Clients button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            ClientsGrid.Visibility = Visibility.Visible;
            Clients.Background = Brushes.LightSkyBlue;

            List<Client> clientList = buyer.FetchClients(1);
            ClientsList.ItemsSource = clientList;
        }

        ///
        /// \brief Used to manage the visibility status of the marketplace screen.
        ///
        /// \return None - void
        ///
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

        ///
        /// \brief Used to manage the display of order list based on radio button selection.
        ///
        /// \return None - void
        ///
        private void Refresh_Orders()
        {
            List<Order> orderList = new List<Order>();

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
            CollectionView viewOrder = (CollectionView)CollectionViewSource.GetDefaultView(OrdersList.ItemsSource);
            viewOrder.SortDescriptions.Add(new SortDescription("OrderID", ListSortDirection.Ascending));
        }

        ///
        /// \brief Event handler for when AllBox radio button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void AllBox_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Orders();
        }

        ///
        /// \brief Event handler for when ActiveBox radio button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void ActiveBox_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Orders();
        }

        ///
        /// \brief Event handler for when CompletedBox radio button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void CompletedBox_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Orders();
        }

        ///
        /// \brief Used to reset the reset the UI and hide all elements.
        ///
        /// \return None - void
        ///
        private void ResetStatus()
        {
            // Hide all modules
            ClientsGrid.Visibility = Visibility.Hidden;
            OrdersGrid.Visibility = Visibility.Hidden;
            MarketPlaceGrid.Visibility = Visibility.Hidden;
            CarriersGrid.Visibility = Visibility.Hidden;
            GenerateInvoice.Visibility = Visibility.Hidden;

            // Reset menu buttons to non-clicked status
            MarketPlace.Background = Brushes.WhiteSmoke;
            Clients.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;

            AllBox.IsChecked = false;
            ActiveBox.IsChecked = false;
            CompletedBox.IsChecked = false;
        }

        ///
        /// \brief Event handler for when AcceptClient radio button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void AcceptClient_Click(object sender, RoutedEventArgs e)
        {
            // Get the current contract list shown in the table
            List<Contract> currentList = ContractsList.ItemsSource.Cast<Contract>().ToList();

            Buyer buyer = new Buyer();

            // Get all the contracts selected and generate order for them and remove from the list
            foreach (object contract in ContractsList.SelectedItems)
            {
                buyer.GenerateOrder((Contract)contract);
                currentList.Remove((Contract)contract);
            }

            // Update the contracts list
            ContractsList.ItemsSource = currentList;
        }

        ///
        /// \brief Event handler for when GenerateInvoice button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void GenerateInvoice_Click(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = (Order)OrdersList.SelectedItem;
            Invoice invoice = buyer.CreateInvoice(selectedOrder);

            DAL db = new DAL();
            // If invoice doesn't exist, create one
            if (!db.IsExistentInvoice(invoice.OrderID))
            {
                db.CreateInvoice(invoice);
            }

            InvoiceInformation inv = new InvoiceInformation(invoice);
            inv.ShowDialog();
        }
    }
}

/* -- FILEHEADER COMMENT --
    FILE		:	PlannerPage.xaml.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the PlannerPage class.
*/

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
using System.ComponentModel;


namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for PlannerPage.xaml
    /// </summary>
    public partial class PlannerPage : Window
    {
        //planner object to access Planner class
        private Planner planner = new Planner();

        ///
        /// \brief This constructor is used to initialize the planner page UI.
        /// 
        public PlannerPage()
        {
            InitializeComponent();

            OrdersPage();        
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
        /// \brief Event handler for when Invoices button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void Invoices_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            ReportsGrid.Visibility = Visibility.Visible;
            Invoices.Background = Brushes.LightSkyBlue;
            AllInvoices.IsChecked = true;
            Refresh_Invoices();
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
            OrdersPage();
        }

        ///
        /// \brief Used to manage display of Orders page in planner UI.
        /// 
        /// \return None - void
        /// 
        private void OrdersPage()
        {           

            AllBox.IsChecked = true;
            Orders.Background = Brushes.LightSkyBlue;
            OrdersGrid.Visibility = Visibility.Visible;
            Refresh_Orders();
        }

        ///
        /// \brief Event handler for when AllInvoices button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void AllInvoices_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Invoices();
        }

        ///
        /// \brief Event handler for when PastInvoices button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void PastInvoices_Click(object sender, RoutedEventArgs e)
        {
            Refresh_Invoices();
        }

        ///
        /// \brief Event handler for when Report button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void Report_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            ReportsGrid.Visibility = Visibility.Visible;
            AllInvoices.IsChecked = true;

            Refresh_Invoices();
        }

        ///
        /// \brief Used to populate the invoice list based on selection.
        /// 
        /// 
        /// \return None - void
        /// 
        private void Refresh_Invoices()
        {
            List<Invoice> invoicesList = new List<Invoice>();

            if(AllInvoices.IsChecked == true)
            {
                // past invoice is also checked, get all invoices
                invoicesList = planner.GenerateSummaryReport(false);           
               
            }
            else if (PastInvoice.IsChecked == true)
            {
                // completed box is checked, fetch only completed orders
                invoicesList = planner.GenerateSummaryReport(true);
            }

            ReportList.ItemsSource = invoicesList;           
        }

        ///
        /// \brief Used to populate the orders list based on selection.
        /// 
        /// 
        /// \return None - void
        /// 
        private void Refresh_Orders()
        {
            var orderList = new List<Order>();

            if(AllBox.IsChecked == true)
            {
                // Get all orders
                orderList = planner.FetchOrders(2);
            }
            else if (ActiveBox.IsChecked == true)
            {
                // Get active orders
                orderList = planner.FetchOrders(0);
            }
            else if(CompletedBox.IsChecked == true)
            {
                // completed box is checked, fetch only completed orders
                orderList = planner.FetchOrders(1);
                
                ViewCarrier.Visibility = Visibility.Hidden;
                CompleteOrder.Visibility = Visibility.Hidden;
                OrderProgress.Visibility = Visibility.Hidden;
            }

            OrdersList.ItemsSource = orderList;
            // sort by OrderID
            CollectionView viewOrder = (CollectionView)CollectionViewSource.GetDefaultView(OrdersList.ItemsSource);
            viewOrder.SortDescriptions.Add(new SortDescription("OrderID", ListSortDirection.Ascending));
        }

        ///
        /// \brief Event handler for when AllBox button is clicked.
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
        /// \brief Event handler for when ActiveBox button is clicked.
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
        /// \brief Event handler for when CompletedBox button is clicked.
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
        /// \brief Event handler for when view carrier button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        ///
        private void View_Carrier(object sender, RoutedEventArgs e)
        {
            Order currentOrder = (Order) OrdersList.SelectedItem;
            List<CarrierCity> carrierCity = planner.GetCarriers(currentOrder.Origin.ToString(), currentOrder.JobType);
            CarrierSelection selectCarrier = new CarrierSelection(carrierCity, currentOrder);
            if (selectCarrier.ShowDialog() == true)
            {
                CompleteOrder.Visibility = Visibility.Hidden;
                OrderProgress.Visibility = Visibility.Hidden;
                ViewCarrier.Visibility = Visibility.Hidden;
                int currentIndex = OrdersList.SelectedIndex;
                Refresh_Orders();
                OrdersList.SelectedItem = OrdersList.Items[currentIndex];    // reselect current order we just processed
            }
        }

        ///
        /// \brief Event handler for when complete order button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        ///
        private void Complete_Order(object sender, RoutedEventArgs e)
        {
            Order currentOrder = (Order)OrdersList.SelectedItem;
            planner.CompleteOrder(currentOrder);
            CompleteOrder.Visibility = Visibility.Hidden;
            OrderProgress.Visibility = Visibility.Hidden;
            Refresh_Orders();

            StringBuilder msg = new StringBuilder();
            msg.AppendLine($"Order Complete! \nOrder #{currentOrder.OrderID} has arrived to its destination!");
            msg.AppendLine($"Client Name: {currentOrder.ClientName}");
            msg.AppendLine($"Order Completion Date: {DateTime.Now}");
            MessageBox.Show(msg.ToString(), "Order Complete", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        ///
        /// \brief Event handler for when simulate order status button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        ///
        private void Simulate_OrderStatus()
        {
            Order currentOrder = (Order)OrdersList.SelectedItem;

            DateTime expectedDeliveryDate = currentOrder.OrderCreationDate.AddHours(planner.GetTotalTime(currentOrder));
            TimeSpan TotalTimeSpan = expectedDeliveryDate - currentOrder.OrderCreationDate;
            TimeSpan TimePassed = DateTime.Now - currentOrder.OrderCreationDate;

            double result = TimePassed.TotalHours * 100 / TotalTimeSpan.TotalHours;
            if(result >= 100)
            {
                result = 100;
                CompleteOrder.Visibility = Visibility.Visible;
                OrderProgress.Visibility = Visibility.Hidden;
            }
            else
            {
                OrderProgress.Visibility = Visibility.Visible;
                CompleteOrder.Visibility = Visibility.Hidden;
                TimeSpan TimeRemaining = expectedDeliveryDate - DateTime.Now;
                HoursLabel.Content = $"Time Left: {(int)TimeRemaining.TotalHours} hrs, {TimeRemaining.Minutes} m";
            }

            OrderProgressBar.Value = result;
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
            if (currentOrder != null)
            {
                if (planner.CarrierAssigned(currentOrder) == true && currentOrder.IsCompleted == 0)
                {
                    // carrier has already been assigned, display complete order button                             
                    ViewCarrier.Visibility = Visibility.Hidden;
                    Simulate_OrderStatus();
                }
                else
                {
                    // carrier has not been assigned, display view carrier button
                    ViewCarrier.Visibility = Visibility.Visible;
                    CompleteOrder.Visibility = Visibility.Hidden;
                    OrderProgress.Visibility = Visibility.Hidden;
                }

                if (currentOrder.IsCompleted == 1)
                {
                    // current order is completed, hide both buttons
                    ViewCarrier.Visibility = Visibility.Hidden;
                    CompleteOrder.Visibility = Visibility.Hidden;
                    OrderProgress.Visibility = Visibility.Hidden;
                }              
            }                
        }

        ///
        /// \brief Used to reset the display of the page and hiding visibily of components.
        /// 
        /// \return None - void
        ///
        private void ResetStatus()
        {
            // Reset all buttons background
            Invoices.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;

            // Hide all grids
            ReportsGrid.Visibility = Visibility.Hidden;
            OrdersGrid.Visibility = Visibility.Hidden;


            ActiveBox.IsChecked = false;
            CompletedBox.IsChecked = false;
            AllInvoices.IsChecked = false;
            PastInvoice.IsChecked = false;

            CompleteOrder.Visibility = Visibility.Hidden;
            OrderProgress.Visibility = Visibility.Hidden;
            ViewCarrier.Visibility = Visibility.Hidden;
        }    
    }
}

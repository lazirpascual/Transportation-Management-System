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
        private Planner planner = new Planner();

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
            resetStatus();

            Reports.Background = Brushes.LightSkyBlue;
        }

        private void Activities_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();

            Activities.Background = Brushes.LightSkyBlue;
        }

        private void Trip_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();

            Trip.Background = Brushes.LightSkyBlue;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();

            Orders.Background = Brushes.LightSkyBlue;
            OrdersList.Visibility = Visibility.Visible;
            ActiveBox.Visibility = Visibility.Visible;

            List<Order> orderList = new List<Order>();
            orderList = planner.FetchOrders(2);
            OrdersList.ItemsSource = orderList;
            OrdersList.Visibility = Visibility.Visible;
            ActiveBox.Visibility = Visibility.Visible;
            CompletedBox.Visibility = Visibility.Visible;
        }

        private void ActiveBox_Click(object sender, RoutedEventArgs e)
        {
            var orderList = new List<Order>();
      
            if (ActiveBox.IsChecked == true)
            {
                // active box is checked
                if (CompletedBox.IsChecked == true)
                {
                    // completed box is also checked, get all orders
                    orderList = planner.FetchOrders(2);
                    ViewCarrier.Visibility = Visibility.Hidden;
                }              
                else
                {
                    // completed box is not checked, fetch only the active orders
                    orderList = planner.FetchOrders(0);
                    ViewCarrier.Visibility = Visibility.Visible;
                }               
            }
            else
            {
                // active box is not checked
                if (CompletedBox.IsChecked == true)
                {
                    // completed box is checked, fetch only completed orders
                    orderList = planner.FetchOrders(1);
                }
                else
                {
                    // completed box is not checked, fetch all orders
                    orderList = planner.FetchOrders(2);
                }
                ViewCarrier.Visibility = Visibility.Hidden;
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
                    orderList = planner.FetchOrders(2);
                }
                else
                {
                    // active box is not checked, get only completed orders
                    orderList = planner.FetchOrders(1);
                }
                ViewCarrier.Visibility = Visibility.Hidden;
            }
            else
            {
                // completed box is not checked
                if (ActiveBox.IsChecked == true)
                {
                    // active box is checked, fetch only active orders
                    orderList = planner.FetchOrders(0);
                    ViewCarrier.Visibility = Visibility.Visible;
                }
                else
                {
                    // active box is also not checked, fetch all orders
                    orderList = planner.FetchOrders(2);
                    ViewCarrier.Visibility = Visibility.Hidden;
                }
            }

            OrdersList.ItemsSource = orderList;
        }

        private void View_Carrier(object sender, RoutedEventArgs e)
        {
            Order currentOrder = (Order) OrdersList.SelectedItem;
            List<CarrierCity> carrierCity = planner.GetCarriers(currentOrder.Origin.ToString(), currentOrder.JobType);
            CarrierSelection selectCarrier = new CarrierSelection(carrierCity, currentOrder.JobType);
            selectCarrier.Show();
        }

        private void resetStatus()
        {
            // reset buttons to non-clicked status
            Reports.Background = Brushes.WhiteSmoke;
            Activities.Background = Brushes.WhiteSmoke;
            Orders.Background = Brushes.WhiteSmoke;
            Trip.Background = Brushes.WhiteSmoke;

            // reset clicking buttons options
            Button1.Visibility = Visibility.Hidden;
            Button2.Visibility = Visibility.Hidden;
            Button3.Visibility = Visibility.Hidden;
            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;
            ViewCarrier.Visibility = Visibility.Hidden;

            // reset previous lists
            //ContractsList.Visibility = Visibility.Hidden;
            //InvoicesList.Visibility = Visibility.Hidden;
            //CarriersList.Visibility = Visibility.Hidden;
            //OrdersList.Visibility = Visibility.Hidden;
            //ClientsList.Visibility = Visibility.Hidden;
        }
    }
}

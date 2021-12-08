/* -- FILEHEADER COMMENT --
    FILE		:	CarrierSelection.xaml.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the carrier selection user interface in Planner accounts.
                    It allows a Planner to view available carriers and select carriers for an order.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for CarrierSelection.xaml
    /// </summary>
    public partial class CarrierSelection : Window
    {
        // Order object
        private readonly Order currentOrder;

        // list of carriers
        private readonly List<string> carriers = new List<string>();

        ///
        /// \brief This constructor is used to initialize the visibility status of components within the carrier selection UI.
        ///
        public CarrierSelection()
        {
            InitializeComponent();
        }

        ///
        /// \brief This overloaded constructor is used to initialize the visibility status of components within the carrier selection UI.
        ///
        /// \param carriers - <b>List<CarrierCity></b> - list of carrier city objects.
        /// \param order - <b>Order</b> - Order object with its functionalities.
        ///
        public CarrierSelection(List<CarrierCity> carriers, Order order)
        {
            InitializeComponent();
            currentOrder = order;
            if (order.JobType == JobType.FTL)
            {
                CreateCarrierFTL(carriers);
            }
            else
            {
                OrderQuantity.Content = $"Remaining Quantity from Order: {currentOrder.Quantity}";
                // Update the remaining quantity
                CreateCarrierLTL(carriers);
            }
        }

        ///
        /// \brief Event handler for when SelectCarrier button is clicked.
        ///
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        ///
        /// \return None - void
        ///
        private void SelectCarrier_Click(object sender, RoutedEventArgs e)
        {
            if (CarrierList.SelectedItem == null)
            {
                MessageBox.Show("Please select a carrier!", "Invalid Carrier", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Planner planner = new Planner();

                // Add carrier to order
                object currentCarrier = CarrierList.SelectedItem;
                if (currentCarrier is FTL)
                {
                    FTL FTLCarrier = (FTL)CarrierList.SelectedItem;
                    planner.SelectOrderCarrier(currentOrder, FTLCarrier.CarrierID);
                    carriers.Add(FTLCarrier.Name);
                }
                else
                {
                    LTL LTLCarrier = (LTL)CarrierList.SelectedItem;
                    planner.SelectOrderCarrier(currentOrder, LTLCarrier.CarrierID);
                    if (!carriers.Contains(LTLCarrier.Name))
                    {
                        carriers.Add(LTLCarrier.Name);
                    }

                    // If current carrier does not have enough availability for the order, select another carrier to fullfill the rest
                    if (LTLCarrier.LTLAval < currentOrder.Quantity)
                    {
                        // Remove from list if the current carrier doesn't have enough availabity
                        CarrierList.Items.Remove(currentCarrier);
                        currentOrder.Quantity -= LTLCarrier.LTLAval;
                    }
                    // If we have more than 26 pallets, we need to have more than 1 trip
                    else if (currentOrder.Quantity > 26)
                    {
                        currentOrder.Quantity -= 26;

                        // Update the remaining quantity of the carrier
                        ((LTL)CarrierList.SelectedItem).LTLAval -= 26;

                        // Refresh Items
                        CarrierList.Items.Refresh();
                    }
                    else
                    {
                        currentOrder.Quantity -= LTLCarrier.LTLAval;
                    }

                    // Update remaining quantity if still have left
                    if (currentOrder.Quantity > 0)
                    {
                        // Update the remaining quantity
                        OrderQuantity.Content = $"Remaining Quantity from Order: {currentOrder.Quantity}";
                    }
                }

                // If multiple trips were selected if needed
                if (currentOrder.Quantity <= 0)
                {
                    DialogResult = true;
                    Close();

                    StringBuilder msg = new StringBuilder();
                    msg.AppendLine($"Order #{currentOrder.OrderID} has been processed!");
                    msg.AppendLine($"Client Name: {currentOrder.ClientName}");
                    msg.AppendLine($"Order Creation Date: {DateTime.Now}");
                    msg.AppendLine($"Carrier(s) Selected: ");
                    foreach (string carrier in carriers)
                    {
                        msg.AppendLine($"    {carrier}");
                    }
                    MessageBox.Show(msg.ToString(), "Order Processed", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        ///
        /// \brief The purpose of this private class is to hold and model all attributes of the carrier with LTL avalability.
        ///
        ///
        /// \author <i>Team Blank</i>
        ///
        private class LTL
        {
            public int CarrierID { get; set; }
            public string Name { get; set; }
            public City DepotCity { get; set; }
            public int LTLAval { get; set; }
            public double LTLRate { get; set; }
        }

        ///
        /// \brief The purpose of this private class is to hold and model all attributes of the carrier with FTL avalability.
        ///
        ///
        /// \author <i>Team Blank</i>
        ///
        private class FTL
        {
            public int CarrierID { get; set; }
            public string Name { get; set; }
            public City DepotCity { get; set; }
            public int FTLAval { get; set; }
            public double FTLRate { get; set; }
            public double ReeferCharge { get; set; }
        }

        ///
        /// \brief Used to create columns for orders that are of type LTL.
        ///
        /// \param carriers  - <b>List<CarrierCity></b> - object that invoked the event handler.
        ///
        /// \return None - void
        ///
        private void CreateCarrierLTL(List<CarrierCity> carriers)
        {
            GridView gridView = new GridView();
            CarrierList.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Carrier Name",
                DisplayMemberBinding = new Binding("Name")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Depot City",
                DisplayMemberBinding = new Binding("DepotCity")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "LTL Availability",
                DisplayMemberBinding = new Binding("LTLAval")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "LTL Rate",
                DisplayMemberBinding = new Binding("LTLRate")
            });

            foreach (CarrierCity carrier in carriers)
            {
                CarrierList.Items.Add(new LTL { CarrierID = (int)carrier.Carrier.CarrierID, Name = carrier.Carrier.Name, DepotCity = carrier.DepotCity, LTLAval = carrier.LTLAval, LTLRate = carrier.Carrier.LTLRate });
            }
        }

        ///
        /// \brief Used to create columns for orders that are of type FTL.
        ///
        /// \param carriers  - <b>List<CarrierCity></b> - object that invoked the event handler.
        ///
        /// \return None - void
        ///
        private void CreateCarrierFTL(List<CarrierCity> carriers)
        {
            GridView gridView = new GridView();
            CarrierList.View = gridView;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Carrier Name",
                DisplayMemberBinding = new Binding("Name")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Depot City",
                DisplayMemberBinding = new Binding("DepotCity")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "FTL Availability",
                DisplayMemberBinding = new Binding("FTLAval")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "FTL Rate",
                DisplayMemberBinding = new Binding("FTLRate")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Reefer Charge",
                DisplayMemberBinding = new Binding("ReeferCharge")
            });

            foreach (CarrierCity carrier in carriers)
            {
                CarrierList.Items.Add(new FTL { CarrierID = (int)carrier.Carrier.CarrierID, Name = carrier.Carrier.Name, DepotCity = carrier.DepotCity, FTLAval = carrier.FTLAval, FTLRate = carrier.Carrier.FTLRate, ReeferCharge = carrier.Carrier.ReeferCharge });
            }
        }
    }
}
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
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        private Admin admin;


        public AdminPage()
        {
            InitializeComponent();
            admin = new Admin();
            resetStatus();
            ConfigurationVisible();
        }

        private void LogFiles_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            AdminLog.Visibility = Visibility.Visible;
            LogFiles.Background = Brushes.LightSkyBlue;

            string logFileName = admin.ViewLogFiles();

            if (logFileName != null)
            {
                AdminLog.Text = File.ReadAllText(logFileName);
            }

        }

        private void Configuration_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Configuration.Background = Brushes.LightSkyBlue;
            ConfigurationVisible();
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Database.Background = Brushes.LightSkyBlue;
            RouteData.Visibility = Visibility.Visible;
            CarrierData.Visibility = Visibility.Visible;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void SelectPath_Click(object sender, RoutedEventArgs e)
        {
            ChangeLogDirectory();
        }

        private void PathUpdate_Click(object sender, RoutedEventArgs e)
        {
            UpdatePath();

        }

        private void ChangeLogDirectory()
        {
            //Variables
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string path = null;

            //Sets the starting directory to the directory of the log files
            dialog.Description = "Select the Directory for the Log Files";

            var result = dialog.ShowDialog();

            if (dialog.SelectedPath != null)
            {
                path = dialog.SelectedPath;
            }

            LogPath.Text = path;

        }

        private void UpdatePath()
        {
            string newPath = LogPath.Text;

            bool result = admin.ChangeLogDirectory(newPath);

            if (result == true)
            {
                System.Windows.MessageBox.Show("Logfile path was successfully updated", "LogFile Path", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Logfile path was not updated, try again", "LogFile Path", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resetStatus()
        {
            // reset buttons
            CarrierData.Visibility = Visibility.Hidden;
            RouteData.Visibility = Visibility.Hidden;

            Button4.Visibility = Visibility.Hidden;
            Button5.Visibility = Visibility.Hidden;
            AddCarrier.Visibility = Visibility.Hidden;
            UpdateCarrier.Visibility = Visibility.Hidden;
            DeleteCarrier.Visibility = Visibility.Hidden;
            Clear.Visibility = Visibility.Hidden;

            //reset any previous lists
            CarrierDatabaseList.Visibility = Visibility.Hidden;
            //InvoicesList.Visibility = Visibility.Hidden;
            //CarriersList.Visibility = Visibility.Hidden;
            //OrdersList.Visibility = Visibility.Hidden;
            //ClientsList.Visibility = Visibility.Hidden;

            //reset menu buttons to non-cliked
            LogFiles.Background = Brushes.WhiteSmoke;
            Database.Background = Brushes.WhiteSmoke;
            Configuration.Background = Brushes.WhiteSmoke;

            CityDatabase.Visibility = Visibility.Hidden;

            AddButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            CarrierName.Visibility = Visibility.Hidden;
            Departure.Visibility = Visibility.Hidden;
            FTLAval.Visibility = Visibility.Hidden;
            LTLAval.Visibility = Visibility.Hidden;
            FTLRate.Visibility = Visibility.Hidden;
            LTLRate.Visibility = Visibility.Hidden;
            Reefer.Visibility = Visibility.Hidden;
            NameLabel.Visibility = Visibility.Hidden;
            DepartureLabel.Visibility = Visibility.Hidden;
            FTLAvalLabel.Visibility = Visibility.Hidden;
            LTLAvalLabel.Visibility = Visibility.Hidden;
            FTLRateLabel.Visibility = Visibility.Hidden;
            LTLRateLabel.Visibility = Visibility.Hidden;
            ReeferChargeLabel.Visibility = Visibility.Hidden;
            AdminLog.Visibility = Visibility.Hidden;

            AdminLog.Visibility = Visibility.Hidden;
            AdminLog.Text = "";
            IPBox.Visibility = Visibility.Hidden;
            PortBox.Visibility = Visibility.Hidden;
            DatabaseConfig.Visibility = Visibility.Hidden;
            IPLabel.Visibility = Visibility.Hidden;
            PortLabel.Visibility = Visibility.Hidden;
            LogLabel.Visibility = Visibility.Hidden;
            LogPath.Visibility = Visibility.Hidden;
            LogPathLabel.Visibility = Visibility.Hidden;
            SelectPath.Visibility = Visibility.Hidden;
            IPUpdate.Visibility = Visibility.Hidden;
            PortUpdate.Visibility = Visibility.Hidden;
            PathUpdate.Visibility = Visibility.Hidden;

            //MainGrid.Visibility = Visibility.Hidden;
        }

        private void CarrierDatabaseVisible(object sender, RoutedEventArgs e)
        {
            CityDatabase.Visibility = Visibility.Visible;
            CarrierDatabaseList.Visibility = Visibility.Visible;
            AddCarrier.Visibility = Visibility.Visible;
            UpdateCarrier.Visibility = Visibility.Visible;
            DeleteCarrier.Visibility = Visibility.Visible;
            Clear.Visibility = Visibility.Visible;
            CarrierName.Visibility = Visibility.Visible;
            Departure.Visibility = Visibility.Visible;
            FTLAval.Visibility = Visibility.Visible;
            LTLAval.Visibility = Visibility.Visible;
            FTLRate.Visibility = Visibility.Visible;
            LTLRate.Visibility = Visibility.Visible;
            Reefer.Visibility = Visibility.Visible;
            NameLabel.Visibility = Visibility.Visible;
            DepartureLabel.Visibility = Visibility.Visible;
            FTLAvalLabel.Visibility = Visibility.Visible;
            LTLAvalLabel.Visibility = Visibility.Visible;
            FTLRateLabel.Visibility = Visibility.Visible;
            LTLRateLabel.Visibility = Visibility.Visible;
            ReeferChargeLabel.Visibility = Visibility.Visible;


            CarrierDatabaseList.SelectedItem = null;

            CarriersFieldsHander(sender, e);
        }

        private void ConfigurationVisible()
        {
            AdminLog.Visibility = Visibility.Visible;
            AdminLog.Text = "";
            IPBox.Visibility = Visibility.Visible;
            PortBox.Visibility = Visibility.Visible;
            DatabaseConfig.Visibility = Visibility.Visible;
            IPLabel.Visibility = Visibility.Visible;
            PortLabel.Visibility = Visibility.Visible;
            LogLabel.Visibility = Visibility.Visible;
            LogPath.Visibility = Visibility.Visible;
            LogPathLabel.Visibility = Visibility.Visible;
            SelectPath.Visibility = Visibility.Visible;
            IPUpdate.Visibility = Visibility.Visible;
            PortUpdate.Visibility = Visibility.Visible;
            PathUpdate.Visibility = Visibility.Visible;

            string logPath = Logger.GetCurrentLogDirectory();
            LogPath.Text = logPath;

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CarrierName.Text = "";
            Departure.Text = "";
            FTLAval.Text = "";
            LTLAval.Text = "";
            FTLRate.Text = "";
            LTLRate.Text = "";
            Reefer.Text = "";
        }


        private void CarriersFieldsHander(object sender, RoutedEventArgs e)
        {
            ClearButton_Click(sender, e);

            // If no option is selected
            if (CarrierDatabaseList.SelectedItems.Count == 0 && CityDatabase.SelectedItems.Count == 0)
            {
                UpdateCarrier.Visibility = Visibility.Hidden;
                DeleteCarrier.Visibility = Visibility.Hidden;

                CityDatabase.ItemsSource = new List<CarrierCity>();
            }
            else
            {
                UpdateCarrier.Visibility = Visibility.Visible;
                DeleteCarrier.Visibility = Visibility.Visible;

                Carrier selectedCarrier = (Carrier)CarrierDatabaseList.SelectedItem;

                if ((sender as System.Windows.Controls.ListView).Name == "CarrierDatabaseList")
                {
                    DAL db = new DAL();
                    try
                    {
                        List<CarrierCity> carriersList = db.FilterCitiesByCarrier(selectedCarrier.Name);
                        CityDatabase.ItemsSource = carriersList;
                    }
                    catch (Exception) { }
                    
                    
                }

                CarrierName.Text = selectedCarrier.Name;
                FTLRate.Text = selectedCarrier.FTLRate.ToString();
                LTLRate.Text = selectedCarrier.LTLRate.ToString();
                Reefer.Text = selectedCarrier.ReeferCharge.ToString();

                // Show details about the city if carrier and city is selected
                if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {
                    Departure.Visibility = Visibility.Visible;
                    FTLAval.Visibility = Visibility.Visible;
                    LTLAval.Visibility = Visibility.Visible;

                    CarrierCity selectedCity = (CarrierCity)CityDatabase.SelectedItem;
                    Departure.Text = selectedCity.DepotCity.ToString();
                    FTLAval.Text = selectedCity.FTLAval.ToString();
                    LTLAval.Text = selectedCity.LTLAval.ToString();
                }

            }
        }

        private void UpdateCarrier_Click(object sender, RoutedEventArgs e)
        {
            string carrierName;
            double _FTLRate;
            double _LTLRate;
            double reefer;

            string newDestination;
            City newCity;
            int newFTL;
            int newLTL;

            // create a carrier object with the values
            Carrier carrier = null;

            CarrierCity carrierCity = null;
            DAL db = new DAL();


            try
            {
                // Get the carrier information from the form
                carrierName = CarrierName.Text;
                _FTLRate = double.Parse(FTLRate.Text);
                _LTLRate = double.Parse(LTLRate.Text);
                reefer = double.Parse(Reefer.Text);


                carrier = new Carrier(carrierName, _FTLRate, _LTLRate, reefer);

                carrier.CarrierID = db.GetCarrierIdByName(carrier.Name);

                // If changing the city
                if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {
                    // Get the city and rates information
                    newDestination = Departure.Text;
                    newCity = (City)Enum.Parse(typeof(City), newDestination, true);
                    newFTL = int.Parse(FTLAval.Text);
                    newLTL = int.Parse(LTLAval.Text);

                    carrierCity = new CarrierCity(carrier, newCity, newFTL, newLTL);
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.");
                return;
            }

            try
            {
                // If only a carrier is selected
                if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 0)
                {
                    db.UpdateCarrier(carrier);

                }
                // If a city and the carrier is selected
                else if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {
                    db.UpdateCarrierCity(carrierCity);
                }


                PopulateCarrierList(sender, e);
            }
            // Inform the user if the operation fails
            catch (ArgumentException exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please try again.");
            }

        }


        private void DeleteCarrier_Click(object sender, RoutedEventArgs e)
        {

            DAL db = new DAL();

            try
            {
                Carrier carrier = (Carrier)CarrierDatabaseList.SelectedItem;
                // If only a carrier is selected
                if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 0)
                {
                    db.DeleteCarrier(carrier);

                    PopulateCarrierList(sender, e);

                    CityDatabase.ItemsSource = new List<CarrierCity>();
                }
                // If a city and the carrier is selected
                else if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {
                    carrier.CarrierID = db.GetCarrierIdByName(carrier.Name);

                    CarrierCity carrierCity = (CarrierCity) CityDatabase.SelectedItem;
                    carrierCity.Carrier = carrier;
                    db.RemoveCarrierCity(carrierCity);

                    PopulateCarrierCitiesList(sender, e);

                }
                else if(CarrierDatabaseList.SelectedItems.Count == 0 && CityDatabase.SelectedItems.Count == 0)
                {
                    System.Windows.MessageBox.Show("Select the Carrier or the City you would like to delete", "No option selected", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            // Inform the user if the operation fails
            catch (ArgumentException exc)
            {
                System.Windows.MessageBox.Show(exc.Message);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void AddCarrier_Click(object sender, RoutedEventArgs e)
        {

            string carrierName;
            double _FTLRate;
            double _LTLRate;
            double reefer;

            string newDestination;
            City newCity;
            int newFTL;
            int newLTL;


            Carrier newCarrier = null;
            CarrierCity newCarrierCity = null;

            DAL db = new DAL();

            try
            {
                // Get the carrier information from the form
                carrierName = CarrierName.Text;
                _FTLRate = double.Parse(FTLRate.Text);
                _LTLRate = double.Parse(LTLRate.Text);
                reefer = double.Parse(Reefer.Text);

                // create a carrier object with the values
                newCarrier = new Carrier(carrierName, _FTLRate, _LTLRate, reefer);
                newCarrier.CarrierID = db.GetCarrierIdByName(newCarrier.Name);
                
                // Get the city and rates information
                newDestination = Departure.Text;
                newCity = (City)Enum.Parse(typeof(City), newDestination, true);
                newFTL = int.Parse(FTLAval.Text);
                newLTL = int.Parse(LTLAval.Text);

                newCarrierCity = new CarrierCity(newCarrier, newCity, newFTL, newLTL);


            }

            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }


            try
            {
                // If carrier exist, create city for that carrier (-1 if it doesnt exist)
                if(db.GetCarrierIdByName(carrierName) != -1)
                {
                    db.CreateCarrierCity(newCarrierCity);
                }
                // If it's a new carrier, create the carrier and the city
                else
                {
                    newCarrier.CarrierID = db.CreateCarrier(newCarrier);
                    db.CreateCarrierCity(newCarrierCity);


                    
                }

                PopulateCarrierList(sender, e);

                // Update the cities list
                List<CarrierCity> carriersList = db.FilterCitiesByCarrier(newCarrier.Name);
                CityDatabase.ItemsSource = carriersList;


            }
            // Inform the user if the operation fails
            catch (ArgumentException exc)
            {
                System.Windows.MessageBox.Show(exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CarrierData_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            CarrierDatabaseVisible(sender, e);

            Database.Background = Brushes.LightSkyBlue;

            PopulateCarrierList(sender, e);
        }


        private void PopulateCarrierList(object sender, RoutedEventArgs e)
        {
            DAL db = new DAL();
            List<Carrier> carriersList = db.GetAllCarriers();
            CarrierDatabaseList.ItemsSource = carriersList;
        }



        private void PopulateCarrierCitiesList(object sender, RoutedEventArgs e)
        {
            DAL db = new DAL();

            Carrier selectedCarrier = (Carrier)CarrierDatabaseList.SelectedItem;

            List<CarrierCity> carriersList = db.FilterCitiesByCarrier(selectedCarrier.Name);
            CityDatabase.ItemsSource = carriersList;
        }

        private void RouteData_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            Database.Background = Brushes.LightSkyBlue;
            RouteDatabaseVisible();

            //List<Route> routeList = new List<Route>; 
            //routeList = routeList.GetRoute();
            //RouteDatabase.ItemsSource = carriersList;
        }

        private void RouteDatabaseVisible()
        {
            RouteDatabase.Visibility = Visibility.Visible;
            RouteDestination.Visibility = Visibility.Visible;
            Km.Visibility = Visibility.Visible;
            Time.Visibility = Visibility.Visible;
            West.Visibility = Visibility.Visible;
            East.Visibility = Visibility.Visible;
            //Destination.Visibility = Visibility.Visible;
            //RouteDatabase.Visibility = Visibility.Visible;
            //RouteDatabase.Visibility = Visibility.Visible;
            //RouteDatabase.Visibility = Visibility.Visible;


        }

        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateRoute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ClearRoute_Click(object sender, RoutedEventArgs e)
        {
            RouteDestination.Text = "";
            Km.Text = "";
            Time.Text = "";
            West.Text = "";
            East.Text = "";

        }

        private void DeleteRoute_Click(object sender, RoutedEventArgs e)
        {

        }

        private void IPUpdate_Click(object sender, RoutedEventArgs e)
        {
            string field = "Server";
            string newData = IPBox.Text;

            DAL db = new DAL();
            try
            {
                db.UpdateDatabaseConnectionString(field, newData);
                System.Windows.MessageBox.Show("IPAddress successfully updated");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please try again.");
            }

        }

        private void PortUpdate_Click(object sender, RoutedEventArgs e)
        {
            string field = "Port";
            string newData = PortBox.Text;

            DAL db = new DAL();
            try
            {
                db.UpdateDatabaseConnectionString(field, newData);
                System.Windows.MessageBox.Show("Port successfully updated");
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please try again.");
            }
        }
    }
}

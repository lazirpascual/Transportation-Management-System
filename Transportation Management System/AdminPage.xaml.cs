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
//using System.Drawing;
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
            ResetStatus();
            ConfigurationVisible();
        }

        private void LogFiles_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
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
            ResetStatus();
            ConfigurationVisible();
        }

        private void Database_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            Database.Background = Brushes.LightSkyBlue;
            DatabaseButtons.Visibility = Visibility.Visible;
        }

        private void Backup_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            Backup.Background = Brushes.LightSkyBlue;
            BackupGrid.Visibility = Visibility.Visible;
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

        private void CarrierData_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            Database.Background = Brushes.LightSkyBlue;
            CarrierGrid.Visibility = Visibility.Visible;
            CarrierDatabaseList.SelectedItem = null;
            CarriersFieldsHander(sender, e);
            PopulateCarrierList(sender, e);
        }

        private void RouteData_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            Database.Background = Brushes.LightSkyBlue;
            RouteGrid.Visibility = Visibility.Visible;

            List<Route> routeList = new List<Route>();
            DAL db = new DAL();
            routeList = db.GetRoutes();
            RouteDatabase.ItemsSource = routeList;
        }

        private void ChangeLogDirectory()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string path = null;

            //Open the directory selection dialog box
            dialog.Description = "Select the Directory for the Log Files";

            dialog.ShowDialog();

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

             
        private void ConfigurationVisible()
        {
            GeneralGrid.Visibility = Visibility.Visible;
            Configuration.Background = Brushes.LightSkyBlue; 
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
                    
                
                if (selectedCarrier != null)
                {
                    try
                    {
                        string caller = (sender as System.Windows.Controls.ListView).Name;
                        if (caller == "CarrierDatabaseList")
                        {
                            DAL db = new DAL();

                            List<CarrierCity> carriersList = db.FilterCitiesByCarrier(selectedCarrier.Name);
                            CityDatabase.ItemsSource = carriersList;

                        }
                    }
                    catch (Exception) { }

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
                else
                {
                    CityDatabase.ItemsSource = new List<CarrierCity>();
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

                // create a carrier object with the values
                carrier = new Carrier(carrierName, _FTLRate, _LTLRate, reefer);
                carrier.CarrierID = db.GetCarrierIdByName(carrier.Name);

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
                System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 0)
                {
                    db.UpdateCarrier(carrier);

                    // Empty Cities list
                    CityDatabase.ItemsSource = new List<CarrierCity>();
                }
                // Show details about the city if carrier and city is selected
                else if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {
                    carrierCity.Carrier.CarrierID = db.GetCarrierIdByName(carrier.Name);
                    db.UpdateCarrierCity(carrierCity);

                    // Update the cities list
                    List<CarrierCity> carriersList = db.FilterCitiesByCarrier(carrier.Name);
                    CityDatabase.ItemsSource = carriersList;
                }

                PopulateCarrierList(sender, e);


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

        private void ResetStatus()
        {
            // Hide all grids
            AdminLog.Visibility = Visibility.Hidden;
            GeneralGrid.Visibility = Visibility.Hidden;
            DatabaseButtons.Visibility = Visibility.Hidden;
            CarrierGrid.Visibility = Visibility.Hidden;
            RouteGrid.Visibility = Visibility.Hidden;
            BackupGrid.Visibility = Visibility.Hidden;

            // Reset all buttons background
            Backup.Background = Brushes.WhiteSmoke;
            LogFiles.Background = Brushes.WhiteSmoke;
            Database.Background = Brushes.WhiteSmoke;
            Configuration.Background = Brushes.WhiteSmoke;
        }

       
        private void UpdateRoute_Click(object sender, RoutedEventArgs e)
        {
            string destination;
            City newDestination;
            City newWest;
            City newEast;
            int distance;
            double time;
            string west;
            string east;
            
            Route route = null;
            
            DAL db = new DAL();

            try
            {
                // Get the route information from the form
                destination = RouteDestination.Text;
                newDestination = (City)Enum.Parse(typeof(City), destination, true);
                distance = int.Parse(Distance.Text);
                time = double.Parse(Time.Text);
                west = West.Text;
                east = East.Text;
                newWest = (City)Enum.Parse(typeof(City), west, true);
                newEast = (City)Enum.Parse(typeof(City), east, true);

                // create a route object with the values
                route = new Route(newDestination, distance, time, newWest, newEast);
                              
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            // Update the Route List
            try
            {
                db.UpdateRoute(route);

                // Empty the route list
                RouteDatabase.ItemsSource = new List<Route>();

                // Reload the updated route list 
                List<Route> routeList = new List<Route>();
                routeList = db.GetRoutes();
                RouteDatabase.ItemsSource = routeList;
               
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

        private void ClearRoute_Click(object sender, RoutedEventArgs e)
        {
            RouteDestination.Text = "";
            Distance.Text = "";
            Time.Text = "";
            West.Text = "";
            East.Text = "";
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

        
        private void RouteDatabase_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ClearButton_Click(sender, e);

            // If no option is selected
            if (RouteDatabase.SelectedItems.Count == 0)
            {
                UpdateRoute.Visibility = Visibility.Hidden;

                RouteDatabase.ItemsSource = new List<Route>();
            }
            else
            {
                UpdateRoute.Visibility = Visibility.Visible;

                Route selectedRoute = (Route)RouteDatabase.SelectedItem;

                RouteDestination.Text = selectedRoute.Destination.ToString();
                Distance.Text = selectedRoute.Distance.ToString();
                Time.Text = selectedRoute.Time.ToString();
                West.Text = selectedRoute.West.ToString();
                East.Text = selectedRoute.East.ToString();

            }
        }

        private void BackupSelect_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            string path = null;

            //Open the directory selection dialog box
            dialog.Description = "Select the Directory for the Backup";

            dialog.ShowDialog();

            // If a path was selected, get the full path
            if (dialog.SelectedPath != null)
            {
                path = dialog.SelectedPath;
            }
            // Show the selected path in the textbox          
            BackupPath.Text = path;
        }

        private void ProcessBackup_Click(object sender, RoutedEventArgs e)
        {
            //Set up the database object 
            DAL db = new DAL();

            // Get the current path for processing the backup
            string backupPath = BackupPath.Text;

            try
            {
                // Process the backup
                db.BackupDatabase(backupPath);
                System.Windows.MessageBox.Show("The backup was succesfully processed!", "Backup Processed", MessageBoxButton.OK, MessageBoxImage.Information);
                BackupDate.Content = DateTime.Now.ToString("MM-dd-yyyy HH:mm");
            }
            // If not successful, inform the user
            catch(ArgumentNullException)
            {
                System.Windows.MessageBox.Show("Please check the Backup path and try again", "Backup Falied", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(Exception)
            {
                System.Windows.MessageBox.Show("Backup was not processed. Please try again.", "Backup Falied", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }      
    }
}
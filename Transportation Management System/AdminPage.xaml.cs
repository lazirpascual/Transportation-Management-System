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
using System.Configuration;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        private readonly Admin admin = new Admin();

        public AdminPage()
        {
            InitializeComponent();
            ResetStatus();
            ConfigurationVisible();
        }

        private void LogFiles_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            AdminLog.Visibility = Visibility.Visible;
            LogFiles.Background = Brushes.LightSkyBlue;

            string logFileName = admin.ViewLogFiles();

            try
            {
                if (logFileName != null)
                {
                    // https://stackoverflow.com/questions/3560651/whats-the-least-invasive-way-to-read-a-locked-file-in-c-sharp-perhaps-in-unsaf
                    using (FileStream fileStream = new FileStream(
                        logFileName,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            AdminLog.Text = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Could not access log file. Please try again later.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void CreateUser_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            UserCreation.Background = Brushes.LightSkyBlue;
            CreateUserGrid.Visibility = Visibility.Visible;
        }

        private void RatesFeesData_Click(object sender, RoutedEventArgs e)
        {
            ResetStatus();
            Database.Background = Brushes.LightSkyBlue;
            RatesGrid.Visibility = Visibility.Visible;
            DAL dal = new DAL();
            var oshRates = dal.GetOSHTRates();
            NewFTLRate.Text = oshRates.RateValuePair[RateType.FTL].ToString();
            NewLTLRate.Text = oshRates.RateValuePair[RateType.LTL].ToString();
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
            routeList = admin.GetRoutesAD();

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
            string oldLogPath = Logger.GetCurrentLogDirectory();
            string newPath = LogPath.Text;

            bool result = admin.ChangeLogDirectory(newPath);

            if (result == true)
            {
                System.Windows.MessageBox.Show("Logfile path was successfully updated", "LogFile Path", MessageBoxButton.OK, MessageBoxImage.Information);
                Logger.Log($"Log directory was moved from \"{oldLogPath}\" to \"{newPath}\"", LogLevel.Information);
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
            GetDatabaseInfo();
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


                            List<CarrierCity> carriersList = admin.GetCitiesByCarrier(selectedCarrier.Name);
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


            Carrier carrier;
            CarrierCity carrierCity = null;



            try
            {
                // Get the carrier information from the form
                carrierName = CarrierName.Text;
                _FTLRate = double.Parse(FTLRate.Text);
                _LTLRate = double.Parse(LTLRate.Text);
                reefer = double.Parse(Reefer.Text);

                // create a carrier object with the values
                carrier = new Carrier(carrierName, _FTLRate, _LTLRate, reefer);
                carrier.CarrierID = admin.FetchCarrierID(carrier.Name);

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
                    admin.UpdateCarrierInfo(carrier);

                    // Empty Cities list
                    CityDatabase.ItemsSource = new List<CarrierCity>();

                    System.Windows.MessageBox.Show($"{carrier.Name} updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Logger.Log($"{carrier.Name} information was updated.", LogLevel.Information);
                }
                // Show details about the city if carrier and city is selected
                else if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {

                    carrierCity.Carrier.CarrierID = admin.FetchCarrierID(carrier.Name);
                    admin.UpdateCity(carrierCity, ((CarrierCity)CityDatabase.SelectedItem).DepotCity);


                    // Update the cities list
                    List<CarrierCity> carriersList = admin.GetCitiesByCarrier(carrier.Name);
                    CityDatabase.ItemsSource = carriersList;

                    System.Windows.MessageBox.Show($"{carrierCity.DepotCity} updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Logger.Log($"{carrier.Name} depot City was updated to {carrierCity.DepotCity}", LogLevel.Information);
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


            try
            {
                Carrier carrier = (Carrier)CarrierDatabaseList.SelectedItem;
                // If only a carrier is selected
                if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 0)
                {
                    var result = System.Windows.MessageBox.Show($"Are you sure you want to delete the carrier {carrier.Name}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        admin.CarrierDeletion(carrier);


                        PopulateCarrierList(sender, e);

                        CityDatabase.ItemsSource = new List<CarrierCity>();

                        System.Windows.MessageBox.Show($"{carrier.Name} deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        Logger.Log($"{carrier.Name} was deleted from the database.", LogLevel.Information);
                    }
                }
                // If a city and the carrier is selected
                else if (CarrierDatabaseList.SelectedItems.Count == 1 && CityDatabase.SelectedItems.Count == 1)
                {
                    carrier.CarrierID = admin.FetchCarrierID(carrier.Name);

                    CarrierCity carrierCity = (CarrierCity)CityDatabase.SelectedItem;
                    carrierCity.Carrier = carrier;

                    var result = System.Windows.MessageBox.Show($"Are you sure you want to delete the carrier city {carrierCity.DepotCity}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        admin.CarrierCity(carrierCity, 0);

                        PopulateCarrierCitiesList(sender, e);


                        System.Windows.MessageBox.Show($"{carrierCity.DepotCity} deleted successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                        Logger.Log($"{carrier.Name} depot City {carrierCity.DepotCity} was deleted from the database.", LogLevel.Information);
                    }

                }
                else if (CarrierDatabaseList.SelectedItems.Count == 0 && CityDatabase.SelectedItems.Count == 0)
                {
                    System.Windows.MessageBox.Show("Select the Carrier or the City you would like to delete", "No option selected", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

            Carrier newCarrier;
            CarrierCity newCarrierCity; ;


            try
            {
                // Get the carrier information from the form
                carrierName = CarrierName.Text;
                _FTLRate = double.Parse(FTLRate.Text);
                _LTLRate = double.Parse(LTLRate.Text);
                reefer = double.Parse(Reefer.Text);

                // create a carrier object with the values
                newCarrier = new Carrier(carrierName, _FTLRate, _LTLRate, reefer);
                newCarrier.CarrierID = admin.FetchCarrierID(newCarrier.Name);

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
                if (admin.FetchCarrierID(carrierName) != -1)
                {
                    admin.CarrierCity(newCarrierCity, 1);

                    // Update the cities list
                    List<CarrierCity> carriersList = admin.GetCitiesByCarrier(newCarrier.Name);
                    CityDatabase.ItemsSource = carriersList;

                    System.Windows.MessageBox.Show($"New carrier depot city {newCarrierCity.DepotCity} created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Logger.Log($"{carrierName} depot city {newCarrierCity.DepotCity} was successfully inserted to the database.", LogLevel.Information);
                }
                // If it's a new carrier, create the carrier and the city
                else
                {
                    newCarrier.CarrierID = admin.CarrierCreation(newCarrier);
                    admin.CarrierCity(newCarrierCity, 1);

                    PopulateCarrierList(sender, e);
                    CityDatabase.ItemsSource = new List<CarrierCity>();

                    System.Windows.MessageBox.Show($"New carrier {newCarrier.Name} created successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Logger.Log($"New carrier {carrierName} was successfully inserted to the database.", LogLevel.Information);
                }

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

            List<Carrier> carriersList = admin.FetchCarriers();
            CarrierDatabaseList.ItemsSource = carriersList;
        }


        private void PopulateCarrierCitiesList(object sender, RoutedEventArgs e)
        {


            Carrier selectedCarrier = (Carrier)CarrierDatabaseList.SelectedItem;

            List<CarrierCity> carriersList = admin.GetCitiesByCarrier(selectedCarrier.Name);
            CityDatabase.ItemsSource = carriersList;
        }

        private void ResetStatus()
        {
            // Hide all grids
            CreateUserGrid.Visibility = Visibility.Hidden;
            AdminLog.Visibility = Visibility.Hidden;
            GeneralGrid.Visibility = Visibility.Hidden;
            DatabaseButtons.Visibility = Visibility.Hidden;
            CarrierGrid.Visibility = Visibility.Hidden;
            RouteGrid.Visibility = Visibility.Hidden;
            BackupGrid.Visibility = Visibility.Hidden;
            RatesGrid.Visibility = Visibility.Hidden;

            // Reset all buttons background
            Backup.Background = Brushes.WhiteSmoke;
            LogFiles.Background = Brushes.WhiteSmoke;
            Database.Background = Brushes.WhiteSmoke;
            Configuration.Background = Brushes.WhiteSmoke;
            UserCreation.Background = Brushes.WhiteSmoke;

            AdminRadio.IsChecked = false;
            PlannerRadio.IsChecked = false;
            BuyerRadio.IsChecked = false;
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
                var result = System.Windows.MessageBox.Show($"Are you sure you want to update the route to {route.Destination}?", "Are you sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    admin.UpdateRouteAD(route);

                    // Reload the updated route list 
                    List<Route> routeList = new List<Route>();
                    routeList = admin.GetRoutesAD();
                    RouteDatabase.ItemsSource = routeList;

                    System.Windows.MessageBox.Show($"Route to {route.Destination} updated successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    Logger.Log($"{route.Destination} was successfully inserted to the database.", LogLevel.Information);
                }

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


        private void DatabaseUpdate_Click(object sender, RoutedEventArgs e)
        {
            string fieldServer = "Server";
            string fieldPort = "Port";
            string fieldUser = "User";
            string fieldPassword = "Password";
            string fieldDatabase = "DatabaseName";
            string newServer;
            string newPort;
            string newUser;
            string newPassword;
            string newDatabase;

            try
            {
                // Get the carrier information from the form
                newServer = ServerBox.Text;
                newPort = PortBox.Text;
                newUser = UserBox.Text;
                newPassword = PasswordBox.Password;
                newDatabase = DatabaseBox.Text;

                // Insert the information to the config file
                admin.UpdateDatabaseConString(fieldServer, newServer);
                admin.UpdateDatabaseConString(fieldPort, newPort);
                admin.UpdateDatabaseConString(fieldUser, newUser);
                admin.UpdateDatabaseConString(fieldPassword, newPassword);
                admin.UpdateDatabaseConString(fieldDatabase, newDatabase);
                System.Windows.MessageBox.Show("Database Information successfully updated", "Database Updated", MessageBoxButton.OK, MessageBoxImage.Information);

                Logger.Log($" Database setting information was successfully updated.", LogLevel.Information);
            }

            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }


        private void PortUpdate_Click(object sender, RoutedEventArgs e)
        {
            string field = "Port";
            string newData = PortBox.Text;
            try
            {
                admin.UpdateDatabaseConString(field, newData);
                System.Windows.MessageBox.Show("Port successfully updated", "Database Updated", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Something went wrong. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

            // Get the current path for processing the backup
            string backupPath = BackupPath.Text;

            try
            {
                // Process the backup
                admin.Backup(backupPath);
                System.Windows.MessageBox.Show("The backup was succesfully processed!", "Backup Processed", MessageBoxButton.OK, MessageBoxImage.Information);
                BackupDate.Content = DateTime.Now.ToString("MM-dd-yyyy HH:mm");

                Logger.Log($"New backup file successfully created at {backupPath}.", LogLevel.Information);
            }
            // If not successful, inform the user
            catch (ArgumentNullException)
            {
                System.Windows.MessageBox.Show("Please check the Backup path and try again", "Backup Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Backup was not processed. Please try again.", "Backup Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateRate_Click(object sender, RoutedEventArgs e)
        {
            double FTLValue;
            double LTLValue;

            RateType type;

            string caller = ((sender as System.Windows.Controls.Button).Name);

            if (caller == "UpdateFTL")
            {
                try
                {
                    FTLValue = double.Parse(NewFTLRate.Text);

                    if (FTLValue <= 0)
                    {
                        System.Windows.MessageBox.Show("FTL value must be greater than 0 to be updated", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        type = RateType.FTL;
                        admin.UpdateRate(FTLValue, type);
                        System.Windows.MessageBox.Show("FTL value was successfully updated", "FTL Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                        Logger.Log($" FTL value was successfully updated to {FTLValue}.", LogLevel.Information);
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

            if (caller == "UpdateLTL")
            {
                try
                {
                    LTLValue = double.Parse(NewLTLRate.Text);

                    if (LTLValue <= 0)
                    {
                        System.Windows.MessageBox.Show("FTL value must be greater than 0 to be updated", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        type = RateType.LTL;
                        admin.UpdateRate(LTLValue, type);
                        System.Windows.MessageBox.Show("LTL value was successfully updated", "LTL Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                        Logger.Log($" FTL value was successfully updated to {LTLValue}.", LogLevel.Information);
                    }
                }
                catch
                {
                    System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }

        }
        

        private void GetDatabaseInfo()
        {
            // Populate the fileds in the admin config page
            string server = ConfigurationManager.AppSettings.Get("Server");
            string port = ConfigurationManager.AppSettings.Get("Port");
            string user = ConfigurationManager.AppSettings.Get("User");
            string password = ConfigurationManager.AppSettings.Get("Password");
            string database = ConfigurationManager.AppSettings.Get("DatabaseName");

            ServerBox.Text = server;
            PortBox.Text = port;
            UserBox.Text = user;
            PasswordBox.Password = password;
            DatabaseBox.Text = database;

        }

        private void UserCreation_Click(object sender, RoutedEventArgs e)
        {
            string firstName;
            string lastName;
            string username;
            string password;
            string email;
            UserRole type;
                       
            try
            {
                // Check if the select box is selected
                if(AdminRadio.IsChecked == true)
                {
                    type = UserRole.Admin;
                }
                else if(PlannerRadio.IsChecked == true)
                {
                    type = UserRole.Planner;
                }
                else if(BuyerRadio.IsChecked == true)
                {
                    type = UserRole.Buyer;
                }
                // No Radio Buttons selected
                else
                {
                    throw new Exception();
                }

                firstName = FirstName.Text;
                lastName = LastName.Text; 
                username = Username.Text;
                password = UserPassword.Password;
                email = Email.Text;

                // Check if all fields has been filled
                if(string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(username) ||
                    string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
                {
                    throw new Exception();
                }

                // Check if the user exists
                DAL db = new DAL();
                if(db.CheckUsername(username))
                {
                    throw new ArgumentException($"User \"{username}\" already exists.");
                }

                User user = new User(firstName, lastName, username, password, email, type);
                if(admin.CreateAUser(user)==true)
                {
                    System.Windows.MessageBox.Show("User successfully added to the system.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);

                    Logger.Log($"{username} was successfully created as a {type}.", LogLevel.Information);
                }
                

                //save it to the database
            }
            catch (ArgumentException ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Duplicate user", MessageBoxButton.OK, MessageBoxImage.Information);
                return;

            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show("Please, make sure that the fields were filled appropriately.", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }        
    }
}
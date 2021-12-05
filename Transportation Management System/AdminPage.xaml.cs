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
        private Carrier carrier;
        public AdminPage()
        {
            InitializeComponent();
            admin = new Admin();
            carrier = new Carrier();
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

            CarrierData.Visibility = Visibility.Visible;
            RouteData.Visibility = Visibility.Visible;
            //RateData.Visibility = Visibility.Visible;
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
            string newPath= LogPath.Text;

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
            //RateData.Visibility = Visibility.Hidden;
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

            AddButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            CarrierID.Visibility = Visibility.Hidden;
            CarrierName.Visibility = Visibility.Hidden;
            Departure.Visibility = Visibility.Hidden;
            FTLAval.Visibility = Visibility.Hidden;
            LTLAval.Visibility = Visibility.Hidden;
            FTLRate.Visibility = Visibility.Hidden;
            LTLRate.Visibility = Visibility.Hidden;
            Reefer.Visibility = Visibility.Hidden;
            label1.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            label4.Visibility = Visibility.Hidden;
            label5.Visibility = Visibility.Hidden;
            label6.Visibility = Visibility.Hidden;
            label7.Visibility = Visibility.Hidden;
            label8.Visibility = Visibility.Hidden;
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
        }

        private void CarrierDatabaseVisible()
        {
            AdminLog.Visibility = Visibility.Visible;
            CityDatabase.Visibility = Visibility.Visible;
            CarrierDatabaseList.Visibility = Visibility.Visible;
            AddCarrier.Visibility = Visibility.Visible;
            UpdateCarrier.Visibility = Visibility.Visible;
            DeleteCarrier.Visibility = Visibility.Visible;
            Clear.Visibility = Visibility.Visible;
            CarrierID.Visibility = Visibility.Visible;
            CarrierName.Visibility = Visibility.Visible;
            Departure.Visibility = Visibility.Visible;
            FTLAval.Visibility = Visibility.Visible;
            LTLAval.Visibility = Visibility.Visible;
            FTLRate.Visibility = Visibility.Visible;
            LTLRate.Visibility = Visibility.Visible;
            Reefer.Visibility = Visibility.Visible;
            label1.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;
            label5.Visibility = Visibility.Visible;
            label6.Visibility = Visibility.Visible;
            label7.Visibility = Visibility.Visible;
            label8.Visibility = Visibility.Visible;

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
            LogPath.Text = Logger.GetCurrentLogDirectory();

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            CarrierID.Text = "";
            CarrierName.Text = "";
            Departure.Text = "";
            FTLAval.Text = "";
            LTLAval.Text = "";
            FTLRate.Text = "";
            LTLRate.Text = "";
            Reefer.Text = "";
        }

        private void UpdateCarrier_Click(object sender, RoutedEventArgs e)
        {
            //update database 
        }

        private void DeleteCarrier_Click(object sender, RoutedEventArgs e)
        {
            //Delete entry from database
        }

        private void AddCarrier_Click(object sender, RoutedEventArgs e)
        {
            //add new entry to database
        }

        private void CarrierData_Click(object sender, RoutedEventArgs e)
        {
            resetStatus();
            CarrierDatabaseVisible();

            Database.Background = Brushes.LightSkyBlue;

            //List<Carrier> carriersList = new List<Carrier>; 
            //carriersList = carrier.GetCarriers();
            //CarrierDatabaseList.ItemsSource = carriersList;
        }

        private void RouteData_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RateData_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

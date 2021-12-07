
/* -- FILEHEADER COMMENT --
    FILE		:	Logger.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the MainWindow UI.
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Transportation_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ///
        /// \brief This constructor is used to initialize the main window UI.
        /// 
        public MainWindow()
        {
            InitializeComponent();
        }

        ///
        /// \brief Event handler for when user presses and drags on screen.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        ///
        /// \brief Event handler for when Button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        ///
        /// \brief Event handler for when Signin button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>RoutedEventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void Signin_Button_Click(object sender, RoutedEventArgs e)
        {
            string loginResult = CheckLogin();
            if (loginResult != null)
            {
                // loginResult is not null, user type is valid
                // go to the page of selected user type
                if (loginResult.Contains("Buyer"))
                {
                    var buyer = new BuyerPage();
                    buyer.Show();
                }
                else if (loginResult.Contains("Planner"))
                {
                    var planner = new PlannerPage();
                    planner.Show();
                }
                else if (loginResult.Contains("Admin"))
                {
                    var admin = new AdminPage();
                    admin.Show();
                }
                App.Current.MainWindow.Hide();
            }
        }


        ///
        /// \brief Used to check the user log in information
        /// 
        /// 
        /// \return user type based on user.
        /// 
        private string CheckLogin()
        {
            DAL auth = new DAL();

            if (UsernameText.Text == "" || PasswordText.Password == "")
            {
                MessageBox.Show("Username and Password field must not be blank", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            else
            {
                if (auth.CheckUsername(UsernameText.Text) == false)
                {
                    // username is invalid
                    MessageBox.Show("This username does not exist. Please enter a valid username", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
                if (auth.CheckUserPassword(UsernameText.Text, PasswordText.Password) == false)
                {
                    // password is invalid
                    MessageBox.Show("This password does not match the username. Please enter a valid password", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    return null;
                }
            }
            string UserType = auth.GetUserType(UsernameText.Text);
        
            return UserType;
        }
    }
}

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
        public MainWindow()
        {
            InitializeComponent();
                       
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

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
                    buyer.ShowDialog();
                }
                else if (loginResult.Contains("Planner"))
                {
                    var planner = new PlannerPage();
                    planner.ShowDialog();
                }
                else if (loginResult.Contains("Admin"))
                {
                    var admin = new AdminPage();
                    admin.ShowDialog();
                }
            }        
        }

        private string CheckLogin()
        {
            Authentication auth = new Authentication();

            if (auth.CheckUsername(UsernameText.Text) == false)
            {
                // username is invalid
                MessageBox.Show("This username does not exist.");
                return null;
            }
            if (auth.CheckUserPassword(UsernameText.Text, PasswordText.Password) == false)
            {
                // password is invalid
                MessageBox.Show("This password is not valid.");
                return null;
            }

            RadioButton[] radioButtons = new RadioButton[] { BuyerRadioButton, PlannerRadioButton, AdminRadioButton };
            /* check if all radio buttons are unchecked */
            if (!radioButtons.Any(rb => rb.IsChecked == true))
            {
                // no radio buttons checked
                MessageBox.Show("Please select a User Type to login.");
                return null;
            }
            else
            {
                // at least one radio button is checked
                foreach (var radioButton in radioButtons)
                {                  
                    if (radioButton.IsChecked == true)
                    {
                        // current button is checked
                        if (auth.CheckUserType(radioButton.Content.ToString(), UsernameText.Text) == true)
                        {
                            // current button matches the user type 
                            return radioButton.Content.ToString();
                        }
                        {
                            // invalid user type
                            MessageBox.Show($"Invalid User Type. Current user is not a {radioButton.Content.ToString()}.");
                            return null;
                        }
                    }                    
                }           
            }
            return null;
        }
    }
}

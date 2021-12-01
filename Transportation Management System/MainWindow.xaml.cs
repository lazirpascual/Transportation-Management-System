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

            bool passed;
            Client client = new Client("Xixibubu");

            DAL db = new DAL();
            try
            {
                db.CreateClient(client);
                passed = true;
            }
            catch (Exception)
            {
                // Fail if an exception is thrown
                passed = false;
            }
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

        private string CheckLogin()
        {
            DAL auth = new DAL();

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
            string UserType = auth.GetUserType(UsernameText.Text);
        
            return UserType;
        }
    }
}

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
    /// Interaction logic for BuyerPage.xaml
    /// </summary>
    public partial class BuyerPage : Window
    {
        public BuyerPage()
        {
            InitializeComponent();           
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void MarketPlace_Click(object sender, RoutedEventArgs e)
        {
            ContractMarketPlace CMP = new ContractMarketPlace();
            List<Contract> contractList = new List<Contract>();
            contractList = CMP.GetContracts();
            lvContracts.ItemsSource = contractList;
            lvContracts.Visibility = Visibility.Visible;
        }
    }
}

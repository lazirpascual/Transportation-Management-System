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
    /// Interaction logic for CarrierSelection.xaml
    /// </summary>
    public partial class CarrierSelection : Window
    {
        public CarrierSelection()
        {
            InitializeComponent();
        }

        public CarrierSelection(List<Carrier> carriers)
        {
            InitializeComponent();
            CarrierList.ItemsSource = carriers;
        }

        private void SelectCarrier_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

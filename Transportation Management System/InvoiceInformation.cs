using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transportation_Management_System
{
    public partial class InvoiceInformation : Form
    {

        public Invoice selectedInvoice = null;
        public InvoiceInformation()
        {
            InitializeComponent();
        }

        public InvoiceInformation(Invoice invoice)
        {
            InitializeComponent();
            order.Text = "Order ID: " + invoice.OrderID;
            clientName.Text = "Client Name: " + invoice.ClientName;
            totalDistance.Text = "Total Distance (Km): " + invoice.TotalKM;
            totalAmount.Text = "Total: " + invoice.TotalAmount;
            origin.Text = "Origin City: " + invoice.Origin;
            destination.Text = "Destination City: " + invoice.Destination;
            selectedInvoice = invoice;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Buyer buyer = new Buyer();
            buyer.SaveInvoice(selectedInvoice);
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            totalAmount.Text = "Total: " + invoice.TotalAmount.ToString("C0");
            origin.Text = "Origin City: " + invoice.Origin;
            destination.Text = "Destination City: " + invoice.Destination;
            selectedInvoice = invoice;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Text file(*.txt)|*.txt";
            saveFile.DefaultExt = ".txt";
            saveFile.Title = "Save Invoice";
            saveFile.FileName= selectedInvoice.ClientName + "-" + selectedInvoice.OrderID + ".txt";
            Random randNum = new Random();
            int invoiceNum = randNum.Next(0, 1000);

            string invoiceText = String.Format("====Sales Invoice====\n" +
                                                "Invoice Number: {0}\n\n" +
                                                "Order Number: {1}\n" +
                                                "Client: {2}\n" +
                                                "Origin City: {3}\n" +
                                                "Destination City: {4}\n" +
                                                "Days taken: {5}\n\n\n" +
                                                "Total: {6}\n", invoiceNum, selectedInvoice.OrderID, selectedInvoice.ClientName, selectedInvoice.Origin, selectedInvoice.Destination, selectedInvoice.Days, selectedInvoice.TotalAmount.ToString("C0"));
            if (saveFile.ShowDialog()==DialogResult.OK)
            {
                File.WriteAllText(saveFile.FileName, invoiceText);
            }
            this.Close();
            string success = "File saved successfully.";
            string title = "File Saved";
            MessageBox.Show(success, title);

        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}


/* -- FILEHEADER COMMENT --
    FILE		:	InvoiceInformation.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the invoice generation user interface in Buyer accounts.
*/

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
    /// 
    /// \class InvoiceInformation
    /// 
    /// \brief The purpose of this class is to manage the code behind of the InvoiceInformation Form.
    ///
    /// This class contains the methods needed to display the list of invoices and to allow the user
    /// to save the invoice to their preferred directory.
    ///
    /// \author <i>Team Blank</i>
    ///
    public partial class InvoiceInformation : Form
    {
        // Invoice object 
        public readonly Invoice selectedInvoice = null;

        ///
        /// \brief This constructor is used to populate the lables in the invoice information window.
        /// 
        /// \param invoice - <b>Invoice</b> - Invoice object with all it's attributes. 
        /// 
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


        ///
        /// \brief Event handler for when SaveButton button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>EventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void SaveButton_Click(object sender, EventArgs e)
        {
            // create a SaveFileDialog object
            SaveFileDialog saveFile = new SaveFileDialog();

            // assign default values to the fields in the dialog
            saveFile.Filter = "Text file(*.txt)|*.txt";
            saveFile.DefaultExt = ".txt";
            saveFile.Title = "Save Invoice";
            saveFile.FileName= selectedInvoice.ClientName + "-" + selectedInvoice.OrderID + ".txt";
            Random randNum = new Random();
            int invoiceNum = randNum.Next(0, 1000);

            // format the information that will be shown in the invoice
            string invoiceText = String.Format("====Sales Invoice====\n" +
                                                "Invoice Number: {0}\n\n" +
                                                "Order Number: {1}\n" +
                                                "Client: {2}\n" +
                                                "Origin City: {3}\n" +
                                                "Destination City: {4}\n" +
                                                "Days taken: {5}\n\n\n" +
                                                "Total: {6}\n", invoiceNum, selectedInvoice.OrderID, selectedInvoice.ClientName, selectedInvoice.Origin, selectedInvoice.Destination, selectedInvoice.Days, selectedInvoice.TotalAmount.ToString("C0"));
           
            // if the dialog opens successfully then save the file there.
            if (saveFile.ShowDialog()==DialogResult.OK)
            {
                File.WriteAllText(saveFile.FileName, invoiceText);
                this.Close();
                string success = "File saved successfully.";
                string title = "File Saved";
                MessageBox.Show(success, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to open save file dialog", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///
        /// \brief Event handler for when CancelButton button is clicked.
        /// 
        /// \param sender  - <b>object</b> - object that invoked the event handler.
        /// \param e  - <b>EventArgs</b> - base class used to pass data to event handler.
        /// 
        /// \return None - void
        /// 
        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

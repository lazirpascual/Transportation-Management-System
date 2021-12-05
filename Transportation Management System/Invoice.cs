using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class Invoice
    /// 
    /// \brief The purpose of this class is to model the invoice class
    ///
    /// This class will demonstrate the attributes members of an Invoice Class. An invoice
    /// is generated after the completion of a delivery and it is calculated based on the total
    /// mileage, amount of pallets and rate per hour/km. 
    /// 
    /// \author <i>Team Blank</i>
    ///
    public class Invoice
    {
        /// The order reference for the Invoice
        public int OrderID { set; get; }

        /// The total amount of the Invoice
        public double TotalAmount { set; get; }

        /// The quantity of pallets in the order that generates the Invoice
        public int PalletQuantity { set; get; }

        /// The total mileage of the trip that relates to the order's Invoice
        public double TotalKM { set; get; }

        /// The total hours of the trip that relates to the order's Invoice
        public double TotalHours
        {
            set; get;
        } 

        /// The rate used to calculate the total amount of the Invoice
        public double Rate
        {
            set; get;
        }  

    }
}

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
    class Invoice
    {
        /// The order reference for the Invoice
        int orderID;

        /// The total amount of the Invoice
        double TotalAmount;

        /// The quantity of pallets in the order that generates the Invoice
        int PalletQuantity;

        /// The total mileage of the trip that relates to the order's Invoice
        double TotalKM;

        /// The total hours of the trip that relates to the order's Invoice
        double TotalHours;

        /// The rate used to calculate the total amount of the Invoice
        double Rate; 

    }
}

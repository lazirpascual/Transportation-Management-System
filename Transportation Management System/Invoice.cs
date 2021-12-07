/* -- FILEHEADER COMMENT --
    FILE		:	Invoice.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Invoice class.
*/

namespace Transportation_Management_System
{
    ///
    /// \class Invoice
    ///
    /// \brief The purpose of this class is to model an Invoice.
    ///
    /// This class will demonstrate the attributes of an Invoice Class. An invoice
    /// is generated after the completion of a delivery and it is calculated based on the total
    /// mileage, amount of pallets and rate per hour/km.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Invoice
    {
        /// The order reference for the Invoice
        public long OrderID { set; get; }

        /// The client name for the Invoice
        public string ClientName { set; get; }

        /// The total amount of the Invoice
        public decimal TotalAmount { set; get; }

        /// The quantity of pallets in the order that generates the Invoice
        public int PalletQuantity { set; get; }

        /// The origin city of the order
        public City Origin { set; get; }

        /// The destination city of the order
        public City Destination { set; get; }

        /// The total distance between origin city to destination
        public int TotalKM { set; get; }

        /// The number of days taken to complete the order
        public double Days { set; get; }
    }
}
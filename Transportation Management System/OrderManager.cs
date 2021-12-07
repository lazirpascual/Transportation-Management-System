using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class OrderManager
    /// 
    /// \brief The purpose of this class is to model the Order Manager class
    ///
    /// This class will demonstrate methods of an Order Management Class. In this class both the
    /// order and the invoice will be created based on the clients demands and process made by the buyer 
    ///  
    /// 
    /// \author <i>Team Blank</i>
    ///
    class OrderManager
    {
        ///
        /// \brief CreateOrder - creates a new order number  
        /// 
        /// \param newContract - <b>Contract</b> - the contract that will contain the details to generate an order
        /// 
        /// \return Order - the order ID object
        ///
        //public int CreateOrder(Contract newContract )
        //{
        
        //}

        ///
        /// \brief GenerateInvoice - creates a new invoice after an order completion  
        /// 
        /// \param orderID - <b>Order</b> - the order identifier that will generate the invoice
        /// 
        /// \return Generated invoice object
        ///
        public Invoice GenerateInvoice(Order orderID)
        {
            Invoice invoice = new Invoice();
            return invoice;
        }
    }
}

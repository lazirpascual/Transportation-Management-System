using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class Buyer
    /// 
    /// \brief The purpose of this class is to represent the Buyer User
    ///
    /// This class represents the role of a buyer User, which represents a User 
    /// who requesting Customer contracts from the Contract Marketplace and generating 
    /// an initial Order or contract
    ///
    /// \author <i>Team Blank</i>
    ///
    class Buyer : User
    {
        ///
        /// \brief Fetch all contracts from the contract marketplace
        /// 
        /// \return A list of all contracts from the marketplace
        /// 
        public List<Contract> FetchContracts() 
        {
            ContractMarketPlace cmp = new ContractMarketPlace();
            List<Contract> cons = cmp.GetContracts();
            return cons;
        }


        ///
        /// \brief Create order based on the contract fetched from the marketplace
        ///
        /// \param contract  - <b>Contract</b> - The selected contract for the order to be created
        /// 
        public void GenerateOrder(Contract contract) { }


        ///
        /// \brief Return a list of all active customers in our system
        /// 
        /// \return A list of all active customers
        /// 
        //public List<Customer> GetActiveCustomers() { }


        ///
        /// \brief Return a list of all active orders in our system
        /// 
        /// \return A list of all active orders
        /// 
        //public List<Order> GetActiveOrders() { }


        ///
        /// \brief Generate an invoice based on an order object
        /// 
        /// \param order  - <b>Order</b> - The order to generate the invoice
        /// 
        /// \return An invoice object
        /// 
        //public Invoice GenerateInvoice(Order order) { }
    }
}

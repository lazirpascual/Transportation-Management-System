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
    public class Buyer : User
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
        /// \return Order object
        public Order GenerateOrder(Contract contract) 
        {
            // Create an order object


            Order order = new Order(contract.ClientName, DateTime.Now, contract.Origin, contract.Destination, contract.JobType, contract.Quantity, contract.VanType);

            // Check if Client exists, If it doesn't exists, create it
            DAL db = new DAL();
            if(db.FilterClientByName(order.ClientName) == null)
            {
                Client client = new Client(order.ClientName);
                db.CreateClient(client);
            }

            // Insert order in db
            try
            {
                // Insert order in database
                db.CreateOrder(order);
            }
            catch(Exception)
            {
                throw;
            }
            
            return order;
        }


        ///
        /// \brief Return a list of all active customers in our system
        /// 
        /// \return A list of all active customers
        /// 
        //public List<Customer> GetActiveCustomers() { }


        ///
        /// \brief Return a list of all active orders in our system
        /// 
        /// \param OnlyActives  - <b>bool</b> - True if only active orders are supposed to be returned
        /// 
        /// \return A list of all active orders
        /// 
        public List<Order> GetOrders(bool OnlyActives = false) 
        {
            List<Order> orderList;

            DAL db = new DAL();
            if (OnlyActives)
            {
                orderList = db.GetActiveOrders();
            }
            else
            {
                orderList = db.GetAllOrders();
            }

            return orderList;
        }


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

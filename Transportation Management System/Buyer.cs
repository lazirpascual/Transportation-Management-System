using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
        /// \brief Return a list of active, completed or all orders in our system
        /// 
        /// \param OnlyActives  - <b>bool</b> - 0 for active, 1 for completed, 2 for all orders
        /// 
        /// \return A list of all active orders
        /// 
        public List<Order> GetOrders(int orderStatus) 
        {
            List<Order> orderList;

            DAL db = new DAL();

            if (orderStatus == 0)
            {
                orderList = db.GetActiveOrders();
            }
            else if (orderStatus == 1)
            {
                orderList = db.GetCompletedOrders();
            }
            else
            {
                orderList = db.GetAllOrders();
            }

            return orderList;
        }


        ///
        /// \brief Inserts a new invoice in the Invoice table
        ///
        /// \param orderObj  - <b>Order</b> - An Order object with all its information
        /// 
        public Invoice CreateInvoice(Order orderObj)
        {
            Invoice invoice = new Invoice();
            Trip tripObj = new Trip();
            long orderID = orderObj.OrderID;
           
            
            int quantity = orderObj.Quantity;
            double totalCost = 0.0;
            int days = tripObj.TotalTime;
            string clientName = orderObj.ClientName;
            string origin = (orderObj.Origin).ToString();
            string destination = (orderObj.Destination).ToString();
            Random randNum = new Random();
            int invoiceNum = randNum.Next(0, 1000);

            string invoiceText = String.Format("Sales Invoice\nInvoice Number: {0}\n\nOrder Number: {1}\nClient: {2}\nOrigin City: {3}\nDestination City: {4}\nDays taken: {5}\n\n\nTotal: {6}\n", invoiceNum, clientName, origin, destination, days, totalCost);
            string invoiceDirectory = Directory.GetCurrentDirectory();
            string invoiceName = invoiceDirectory + "\\"+clientName+"-" + orderID + ".txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(invoiceName))
                {
                    writer.Write(invoiceText);
                }
            }
            catch (Exception)
            {
                throw;
            }

            invoice.OrderID = orderID;
            invoice.PalletQuantity = quantity;
            invoice.TotalAmount = totalCost;
            invoice.ClientName = clientName;
            invoice.Origin = (City)Enum.Parse(typeof(City), origin, true);
            invoice.Destination= (City)Enum.Parse(typeof(City), destination, true);
            invoice.Days = days;

            return invoice;


        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class Planner
    /// 
    /// \brief The purpose of this class is to represent the Planner User
    ///
    /// This class represents the role of a Planner User, which represents a User, who is responsible
    /// for planning for an order and completing an order. Additionally, a Planner can also generate
    /// a summary report of all invoice data for either all time or the past 2 weeks' of simulated time.
    ///
    /// \author <i>Team Blank</i>
    ///
    class Planner : User
    {
        ///
        /// \brief This method calls a query to to the orders database to fetch all active, compeleted, or all orders from the buyer. 
        /// 
        /// \param orderStatus  - <b>int</b> - 0 for active, 1 for completed, 2 for all orders
        /// 
        /// \return Returns list of all fetched orders
        /// 
        public List<Order> FetchOrders(int orderStatus) 
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
        /// \brief Used to select carriers from targeted cities to complete an Order. This 
        /// adds a "trip" to the order for each carrier selected
        /// 
        
        ///
        /// \return Returns void
        /// 
        public void SelectOrderCarrier(Carrier carrierToSelect) { }



        ///
        /// \brief Used to confirm and finalize an order. Completed orders are marked for follow up from the buyer.
        /// 
        /// \return Returns void
        /// 
        public void CompleteOrder(Order order)
        {
            DAL db = new DAL();

            db.CompleteOrder(order);
        }



        ///
        /// \brief Used to confirm and finalize an order. Completed orders are marked for follow up from the buyer.
        /// 
        /// \return Returns void
        /// 
        public List<Carrier> GetCarriers(string originCity)
        {
            DAL db = new DAL();
            List<Carrier> carriers = db.FilterCarriersByCity((City)Enum.Parse(typeof(City), originCity, true));
            return carriers;
        }



        ///
        /// \brief Used to display a list of invoices based on time period
        /// 
        /// \param timeperiod  - <b>bool</b> - true = past 2 weeks only, false = all time.
        ///
        /// \return Returns summmary report of invoice data
        /// 
        public List<Invoice> GenerateSummaryReport(bool timePeriod)
        {
            DAL invoiceList = new DAL();
            Buyer buyer = new Buyer();

            List<Order> orders = invoiceList.FilterCompletedOrdersByTime(timePeriod);
            List<Invoice> invoices = new List<Invoice>();
            foreach (var inv in orders)
            {

                invoices.Add(buyer.CreateInvoice(inv));
            }

            return invoices;

        }
    }

}

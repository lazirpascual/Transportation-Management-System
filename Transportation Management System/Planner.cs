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
    /// This class represents the role of an Admin User, which represents a User who has IT experience
    /// and is tasked with the configuration, maintenance, and troubleshooting of the TMS application.
    ///
    /// \author <i>Team Blank</i>
    ///
    class Planner : User
    {
        ///
        /// \brief This method calls a query to to the orders database to fetch all orders from the buyer. 
        /// 
        /// \return Returns list of all fetched orders
        /// 
        public List<Order> FetchOrders() { }

        ///
        /// \brief Used to select carriers from targeted cities to complete an Order. This 
        /// adds a "trip" to the order for each carrier selected
        /// 
        /// \return Returns void
        /// 
        public void SelectOrderCarrier(Carrier carrierToSelect) { }

        ///
        /// \brief Used to confirm and finalize an order. Completed orders are marked for follow up
        /// adds a "trip" to the order for each carrier selected
        /// 
        /// \return Returns void
        /// 
        public void CompleteOrder(Carrier carrierToSelect) { }




    }

}

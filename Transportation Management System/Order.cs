using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class Order
    /// 
    /// \brief The purpose of this class is to model the Order class
    ///
    /// This class will demonstrate the attributes members and methods of an Order Class. An order
    /// is generated based on the client demand and after receiving all the detail information of the trip and
    /// quantity to be delivered. The class will contain properties for each of the data members. 
    /// 
    /// \author <i>Team Blank</i>
    ///
    public class Order
    {
        /// The identifier number of the order 
        public int OrderID;

        /// The client related the order
        public string ClientName;

        /// The date when the order was created
        public DateTime OrderCreationDate;

        /// The origin of the order's trip
        public City Origin;

        /// The destination of the order's trip
        public City Destination;

        /// The type of job to be completed
        public int JobType;

        /// The van type of the job to be completed
        public int VanType;

        /// The quantity of pallets contained in the order
        public int Quantity;

        /// The indicator of current and completed orders
        public int IsCompleted; 


        public Order(string clientName, DateTime creationTime, City origin, City destination, int jobType, int quantity, int vanType)
        {
            ClientName = clientName;
            OrderCreationDate = creationTime;
            Origin = origin;
            Destination = destination;
            JobType = jobType;
            Quantity = quantity;
            VanType = vanType;
            IsCompleted = 0;
        }

        public Order() {}


        ///
        /// \brief SetClient defines a client number for a new client registered in the system
        /// 
        /// \param clientName  - <b>string</b> - to link a client to a specified order 
        /// 
        /// \return Client 
        ///
        //public Client SetClient(string clientName)
        //{

        //}

        ///
        /// \brief Determine the trip of the order based on the origin and destination
        /// 
        /// \param destination  - <b>string</b> - the final point of the delivery trip
        /// \param origin - <b>string</b> - the starting point of the delivery trip
        /// 
        /// \return void
        ///
        //public bool CreateTrip(string destination, string origin)
        //{

        //}
    }
}

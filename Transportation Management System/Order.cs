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
        public int OrderID { get; set; }

        /// The client related the order
        public string ClientName { get; set; }

        /// The date when the order was created from the marketplace
        public DateTime OrderCreationDate { get; set; }

        /// The date when the order was 
        public DateTime OrderAcceptedDate { get; set; }

        /// The origin of the order's trip
        public City Origin { get; set; }

        /// The destination of the order's trip
        public City Destination { get; set; }

        /// The type of job to be completed
        public JobType JobType { get; set; }

        /// The van type of the job to be completed
        public VanType VanType { get; set; }

        /// The quantity of pallets contained in the order
        public int Quantity { get; set; }

        /// The indicator of current and completed orders
        public int IsCompleted { get; set; }

        /// The indicator of whether invoice has been generated for this order or not
        public int InvoiceGenerated { get; set; }

        /// The date when the order was completed
        public DateTime OrderCompletionDate { get; set; }


        public Order(string clientName, City origin, City destination, JobType jobType, int quantity, VanType vanType)
        {
            ClientName = clientName;
            Origin = origin;
            Destination = destination;
            JobType = jobType;
            Quantity = quantity;
            VanType = vanType;
            IsCompleted = 0;
            InvoiceGenerated = 0;
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

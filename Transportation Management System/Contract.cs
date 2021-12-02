using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transportation_Management_System
{
    public enum JobType
    {
        FTL,
        LTL
    }
    public enum VanType
    {
        DryVan,
        Reefer
    }
    /// 
    /// \class Contract
    /// 
    /// \brief The purpose of this class is to model the contract class
    ///
    /// This class will demonstrate the attributes of a Contract Class. A contract is generated
    /// based on the client demand and after receiving all the detail information of the trip and
    /// quantity to be delivered. The class will contain properties for each of the data members. 
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Contract
    {
        /// Gets and Sets the contract's Client Name
        public string ClientName { get; set; }

        /// Gets and Sets the contract's JobType
        public JobType JobType { get; set; }

        /// Gets and Sets the contract's Quantity
        public int Quantity { get; set; }

        /// Gets and Sets the contract's Origin
        public City Origin { get; set; }

        /// Gets and Sets the contract's Destination
        public City Destination { get; set; } 

        /// Gets and Sets the contract's VanType
        public VanType VanType { get; set; }


        // Create a Contract Constructor
    }
}

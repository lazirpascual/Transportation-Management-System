using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{

    /// 
    /// \class Client
    /// 
    /// \brief The purpose of this class is to represent a Client object
    ///
    /// A Client will be specified in the contract, fetched from the contract marketplace
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Client
    {
        /// The ID of the client
        public int ClientID { get; set; }

        /// The client full name
        public string ClientName { get; set; } 


        /// Create client Constructor
        public Client(string name)
        {
            ClientName = name;
        }
    }
}

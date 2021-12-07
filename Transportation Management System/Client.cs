
/* -- FILEHEADER COMMENT --
    FILE		:	Client.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Client class.
*/

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
    /// \brief The purpose of this class is to represent a Client.
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

        /// used to describe whether the client is active or not
        public int IsActive { get; set; }

        ///
        /// \brief This Client class constructor is used to initialize the name property of the client.
        /// 
        public Client(string name)
        {
            ClientName = name;
        }

        ///
        /// \brief This Client class constructor is used to access a client with empty properties.
        /// 
        public Client() { }
    }
}

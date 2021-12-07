
/* -- FILEHEADER COMMENT --
    FILE		:	Contract.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Contract class.
*/

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
    ///
    ///  Enum to convert JobType from int to actual job type (FTL/LTL).
    ///
    public enum JobType
    {
        FTL,
        LTL
    }

    ///
    ///  Enum to convert VanType from int to actual van type (DryVan/Reefer).
    ///
    public enum VanType
    {
        DryVan,
        Reefer
    }
    /// 
    /// \class Contract
    /// 
    /// \brief The purpose of this class is to model a contract fetched from the marketplace.
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Contract
    {
        /// Name of the client
        public string ClientName { get; set; }

        /// JobType mentioned in the contract
        public JobType JobType { get; set; }

        /// Quantity of product in contract
        public int Quantity { get; set; }

        /// Origin city of the contract
        public City Origin { get; set; }

        /// Destination city of the contract
        public City Destination { get; set; } 

        /// VanType needed for the contract
        public VanType VanType { get; set; }
    }
}

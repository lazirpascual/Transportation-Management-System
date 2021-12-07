
/* -- FILEHEADER COMMENT --
    FILE		:	Carrier.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Carrier class.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class Carrier
    /// 
    /// \brief The purpose of this class is to hold and model all attributes of the carrier.
    ///
    /// This class will simply keep the updated attributes of the carrier.
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Carrier
    {
        /// The ID of the carrier company
        public long CarrierID { set; get; }

        /// The name of the carrier company
        public string Name { set; get; }

        /// The amount per km for full truckload
        public double FTLRate { set; get; }

        /// The amount per pallet per km for less than truckloads
        public double LTLRate { set; get; }

        /// The amount for reefer van type
        public double ReeferCharge { set; get; }

        /// The status of the carrier in the system
        public bool IsActive { set; get; }


        ///
        /// \brief This Carrier class constructor is used to initialize the properties of the carrier.
        /// 
        public Carrier(string newName, double newFTL, double newLTL, double newReefer)
        {
            Name = newName;
            FTLRate = newFTL;
            LTLRate = newLTL;
            ReeferCharge = newReefer;
        }


        ///
        /// \brief This overloaded Carrier class constructor is used to access a carrier with empty attributes.
        /// 
        public Carrier() { }
                


    }
}

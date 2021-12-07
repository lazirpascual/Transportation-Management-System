
/* -- FILEHEADER COMMENT --
    FILE		:	Carrier.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the CarrierCity class.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class CarrierCity
    /// 
    /// \brief The purpose of this class is to hold and model all attributes of the carrier city.
    ///
    /// This class will simply keep the updated attributes of the carrier such as its depot city and avalabities.
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    public class CarrierCity
    {
        // Carrier of that city
        public Carrier Carrier { set; get; }

        /// The city of the carrier company
        public City DepotCity { set; get; }

        /// The number of full truckloads available
        public int FTLAval { set; get; }

        /// The number of less than truckloads available
        public int LTLAval { set; get; }


        ///
        /// \brief This overloaded CarrierCity class constructor is used to access a carrier city with empty attributes.
        /// 
        public CarrierCity() { }

        ///
        /// \brief This CarrierCity class constructor is used to initialize the properties of the carrier city.
        /// 
        public CarrierCity(Carrier newCarrier, City newDepot, int newFTL, int newLTL)
        {
            Carrier = newCarrier;
            DepotCity = newDepot;
            FTLAval = newFTL;
            LTLAval = newLTL;
        }
    }
}

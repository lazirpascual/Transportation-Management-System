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
    /// \brief The purpose of this class is to hold and model all attributes of the carrier
    ///
    /// This class will simply keep the updated attributes of the carrier that are fetched by
    /// the CarrierManager class methods.
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    class Carrier
    {
        /// The name of the carrier company
        public string name;

        /// The number of full truckloads available
        public int fTLAval;

        /// The number of less than truckloads available
        public int lTLAval;

        /// The amount per km for full truckload
        public double fTLRate;

        /// The amount per pallet per km for less than truckloads
        public double lTLRate;

        /// The amount for reefer van type
        public double reeferCharge; 

    }
}

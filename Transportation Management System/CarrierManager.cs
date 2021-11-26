using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class CarrierManager
    /// 
    /// \brief The purpose of this class is to fetch information about the carriers from the database 
    ///
    /// The methods in this class will fetch rates and load type availabilites from the database and output them.
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    class CarrierManager
    {
        ///
        /// \brief This method will fetch the number of full truckloads available from the database
        ///
        /// \param carrierID  - <b>int</b> - The carrier ID
        /// 
        /// \return ftlAvail - <b>int</b> - The number of full truckloads available for this carrier
        /// 
        public int FetchFTLAvailability(int carrierID)
        {

        }


        ///
        /// \brief This method will fetch the number of less than truckloads available from the database
        ///
        /// \param carrierID  - <b>int</b> - The carrier ID
        /// 
        /// \return ltlAvail - <b>int</b> - The number of less than truckloads available for this carrier
        /// 
        public int FetchLTLAvailability(int carrierID)
        {

        }

        ///
        /// \brief This method will fetch the rate for FTL from the database
        ///
        /// \param carrierID  - <b>int</b> - The carrier ID
        /// 
        /// \return fltRate - <b>double</b> - The rate for FTL for this carrier
        /// 
        public double GetFTLRate()
        {

        }


        ///
        /// \brief This method will fetch the rate for LTL from the database
        ///
        /// \param carrierID  - <b>int</b> - The carrier ID
        /// 
        /// \return lltRate - <b>double</b> - The rate for LTL for this carrier
        /// 
        public double GetLTLRate()
        {

        }


        ///
        /// \brief This method will fetch the rate for reefer van from the database
        ///
        /// \param carrierID  - <b>int</b> - The carrier ID
        /// 
        /// \return reeferRate - <b>double</b> - The rate for reefer rate for this carrier
        /// 
        public double GetReeferCharge()
        {

        }
    }
}

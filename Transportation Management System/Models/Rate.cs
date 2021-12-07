using System.Collections.Generic;

namespace Transportation_Management_System
{
    public enum RateType
    {
        FTL,
        LTL
    }

    /// 
    /// \class OSHTRates
    /// 
    /// \brief The purpose of this class is to hold the OSHTRates 
    ///
    /// This class will hold the rated that will be added on top of the
    /// FTL and LTL rates
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Rate
    {

        /// A dictionary with the rate type and value
        public Dictionary<RateType, double> RateValuePair { get; set; }

        public Rate() { }

        public Rate(RateType newType, double newValue)
        {
            RateValuePair.Add(newType, newValue);
        }
    }
}

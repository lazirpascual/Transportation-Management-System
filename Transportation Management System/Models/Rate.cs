/* -- FILEHEADER COMMENT --
    FILE		:	Rate.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the Rate class.
*/

using System.Collections.Generic;

namespace Transportation_Management_System
{
    /// <summary>
    /// Enum to convert rate type from int to string.
    /// </summary>
    public enum RateType
    {
        FTL,
        LTL
    }

    ///
    /// \class Rate
    ///
    /// \brief The purpose of this class is to hold the OSHTRates
    ///
    /// This class will hold the rates that will be added on top of the
    /// FTL and LTL rates
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Rate
    {
        /// A dictionary with the rate type and value
        public Dictionary<RateType, double> RateValuePair { get; set; }

        ///
        /// \brief This overloaded Rate class constructor is used to access a Rate class with empty attributes.
        ///
        public Rate()
        { }

        ///
        /// \brief This Rate class constructor is used to values to the dictionary as key value pairs.
        ///
        public Rate(RateType newType, double newValue)
        {
            RateValuePair.Add(newType, newValue);
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{

    /// 
    /// \class Trip
    /// 
    /// \brief The purpose of this class is to model the trip required for the order
    ///
    /// This class will demonstrate the attributes and behaviours of the Trip. It will calculate the total
    /// distance and the total time required for the trip.
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    class Trip
    {
        /// The order ID for the Trip
        public int orderID;

        /// The starting city for the transport
        public int originCity;

        /// The destination city for the transport
        public int destinationCity;

        /// The total number of hours needed for the trip
        public double totalTime;

        /// The total number of hours spent driving
        public double hoursDriven;

        /// The total number of hours spent unloading the truck
        public double hoursUnloading;

        /// The total number of hours spent loading the truck
        public double hoursLoading;     

        ///
        /// \brief This method will calculate the route for the trip, based on origin, destination and jobtype
        ///
        /// \param originCity  - <b>int</b> - Where the delivery will start from.
        /// \param destinationCity  - <b>int</b> - Where the delivery needs to reach.
        /// \param jobType  - <b>int</b> - Full truck load or less than truck load
        /// 
        /// \return void
        /// 
        public void CalculateTrip()
        {

        }

        ///
        /// \brief This method will calculate the total distance that the transport will travel
        ///
        /// \param originCity  - <b>int</b> - Where the delivery will start from.
        /// \param destinationCity  - <b>int</b> - Where the delivery needs to reach.
        /// 
        /// \return totalDistance - <b>int</b> - Total distance in kms
        /// 
        public double CalculateDistance()
        {
 
        }

        ///
        /// \brief This method will calculate the total time that the transport will take
        ///
        /// \param hoursLoading  - <b>double</b> - Number of hours needed to load the truck
        /// \param hoursUnloading  - <b>double</b> - Total number of hours needed to unload the truck
        /// \param hoursDriven - <b>double</b> - Total number of hours spent driving
        /// 
        /// 
        /// \return totalTime - <b>double</b> - Total time needed for the transportation
        ///
        public double CalculateTime()
        {

        }

    }



}
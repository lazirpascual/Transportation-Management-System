﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    public enum City
    {
        Null = -1,
        Windsor,
        London,
        Hamilton,
        Toronto,
        Oshawa,
        Belleville,
        Kingston,
        Ottawa,
    }

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
    public class Trip
    {
        /// The trip ID for the Trip
        public long TripID { get; set; }
        /// The order ID for the Trip
        public long OrderID { get; set; }

        /// The carrier ID for the Trip
        public long CarrierID { get; set; }
        /// The starting city for the transport
        public City OriginCity { get; set; }

        /// The destination city for the transport
        public City DestinationCity { get; set; }

        /// The total distance for the transport
        public double TotalDistance { get; set; }

        /// The total number of days needed for the trip
        public double TotalTime { get; set; }

        public JobType JobType { get; set; }

        public VanType VanType { get; set; }



        ///
        /// \brief This method will calculate the total cost for all trips
        ///
        /// \param trips  - <b>List<Trip></b> - List of trips.
        /// 
        /// \return Total cost for the order
        /// 
        public static decimal CalculateTotalCostTrips(List<Trip> trips)
        {
            double totalCost = 0.0;
            DAL db = new DAL();

            // Iterate through the trips and sum the costs of each
            foreach(var trip in trips)
            {
                Carrier currentTripCarrier = db.FilterCarriersByID(trip.CarrierID);

                // Calculate the final price based on the carrier rates and OSHT charge
                switch (trip.JobType)
                {
                    case JobType.FTL:
                        totalCost =  (currentTripCarrier.FTLRate * 1.05) * trip.TotalDistance;
                        break;
                    case JobType.LTL:
                        totalCost =  (currentTripCarrier.LTLRate * 1.08) * trip.TotalDistance;
                        break;
                }


                // Calculate the Reefer charge
                switch(trip.VanType)
                {
                    case VanType.Reefer:
                        // Percentage on top of the cost if it's a reefer van
                        totalCost *= currentTripCarrier.ReeferCharge;
                        break;
                    // If dryvan, only the regular rates
                    case VanType.DryVan:
                        break;
                }
            }

            return (decimal) totalCost;
        }

        ///
        /// \brief This method will calculate the total distance that the transport will travel
        ///
        /// \param originCity  - <b>int</b> - Where the delivery will start from.
        /// \param destinationCity  - <b>int</b> - Where the delivery needs to reach.
        /// 
        /// \return totalDistance - <b>int</b> - Total distance in kms
        /// 
        //public double CalculateDistance()
        //{

        //}

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
        //public double CalculateTime()
        //{

        //}


        ///
        /// \brief Used to calculate the total distance and time between two cities based on the routes table
        ///
        /// \param order  - <b>Order</b> - Order to calculate the distance
        /// 
        /// \return A keyValuePair with the distance and time between those two cities
        /// 
        public void CalculateDistanceAndTime()
        {
            City origin = OriginCity;
            City destination = DestinationCity;
            JobType jb = JobType;

            int totalDistance = 0;
            double totalTime = 0.0;

            // Hours worked in the whole day
            double dailyDrivingTime = 0.0;
            double dailyTotalTime = 0.0;

            // Hours worked in the route (between 2 cities)
            double partialDrivingTime = 0.0;
            double partialTotalTime = 0.0;

            // Check if origin and destination are the same, return 0
            if (origin == destination)
            {
                TotalTime = 0.0;
                TotalDistance = 0;

                return;
            }

            DAL db = new DAL();
            List<Route> routes = db.GetRoutes();

            // Get the current city
            Route curr = routes[(int)origin];
            City lastCity = City.Null;

            // Do the calculation while we're didn't pass destination city or at the end of the route table
            do
            {
                totalDistance += curr.Distance;

                // Driving..
                partialDrivingTime += curr.Time;

                // If in origin or destination or ltl + 2
                if (curr.Destination == origin || curr.Destination == destination || jb == JobType.LTL)
                {
                    // Load, Unload and stop time
                    partialTotalTime += 2;
                }


                // Add total time driven to total time worked
                partialTotalTime += partialDrivingTime;

                // Add the daily working time
                dailyTotalTime += partialTotalTime;
                dailyDrivingTime += partialDrivingTime;

                // If the total performing hours surpassed 8 hours
                if (dailyTotalTime >= 8)
                {
                    double overworkedHours;

                    // Check if the driver is driving more than allowed. If he is, take a break
                    if (dailyDrivingTime >= 8)
                    {
                        // Get the number of hours overworked today
                        overworkedHours = dailyDrivingTime - 8;

                        // Add the number of hours allowed for the day
                        totalTime += partialDrivingTime - overworkedHours;

                        // Wait until next day (24-8)
                        totalTime += 16;

                        // Add the remaining hours for the next day
                        totalTime += overworkedHours;

                        // New Day, new hours
                        dailyTotalTime = 0;
                        dailyDrivingTime = 0;

                    }
                    // The driver operated more than 12 hours
                    else if (dailyTotalTime >= 12)
                    {
                        // Get the number of hours overworked
                        overworkedHours = dailyTotalTime - 12;

                        // Add the number of allowed hours for the day
                        totalTime += partialTotalTime - overworkedHours;

                        // Wait until next day (24-12)
                        totalTime += 12;

                        // Add the remaining hours for the next day
                        totalTime += overworkedHours;

                        // New Day, new hours
                        dailyTotalTime = 0;
                        dailyDrivingTime = 0;

                    }
                    // If the total time is greater than 8, but the driver hasn't driven the total 8 
                    // neither worked a total of 12 hours, just keep going to the next city
                    else
                    {
                        totalTime += partialTotalTime;
                    }

                }
                // If everything is under the limits, just keep going
                else
                {
                    totalTime += partialTotalTime;
                }

                // New route, New Partial hours
                partialDrivingTime = 0;
                partialTotalTime = 0;

                // Going east
                if (origin < destination)
                {
                    curr = curr.EastPtr;
                    if (curr != null) lastCity = curr.West;
                }
                // Goint west
                else if (destination < origin)
                {
                    curr = curr.WestPtr;
                    if (curr != null) lastCity = curr.East;
                }

                // Keep going until we're not at the end of the line and past the destination city
            } while (curr != null && lastCity != destination);


            // Populate fields
            TotalTime = totalTime;
            TotalDistance = totalDistance;
        }

    }
}

using System;
using System.Collections.Generic;

namespace Transportation_Management_System
{
    /// 
    /// \class TripManager
    /// 
    /// \brief The purpose of this class is to perform the calculations for the trip
    ///
    /// This class will perform the calculations related to the trips such as 
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    public class TripManager
    {

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

            Rate OSHTRates = db.GetOSHTRates();

            // Iterate through the trips and sum the costs of each
            foreach (Trip trip in trips)
            {
                Carrier currentTripCarrier = db.FilterCarriersByID(trip.CarrierID);
                double OSHTRate;

                // Calculate the final price based on the carrier rates and OSHT charge
                switch (trip.JobType)
                {
                    case JobType.FTL:
                        OSHTRate = 1 + OSHTRates.RateValuePair[RateType.FTL];
                        totalCost = (currentTripCarrier.FTLRate * OSHTRate) * trip.TotalDistance;
                        break;
                    case JobType.LTL:
                        OSHTRate = 1 + OSHTRates.RateValuePair[RateType.LTL];
                        totalCost = (currentTripCarrier.LTLRate * 1.08) * trip.TotalDistance;
                        break;
                    default:
                        throw new ArgumentException("Trip must contain a job type");
                }


                // Calculate the Reefer charge
                switch (trip.VanType)
                {
                    case VanType.Reefer:
                        // Percentage on top of the cost if it's a reefer van
                        totalCost *= currentTripCarrier.ReeferCharge;
                        break;
                    // If dryvan, only the regular rates
                    case VanType.DryVan:
                        break;
                    default:
                        throw new ArgumentException("Trip must contain a job type");
                }
            }

            return (decimal)totalCost;
        }



        ///
        /// \brief Used to calculate the total distance and time between two cities based on the routes table
        ///
        /// \param order  - <b>Order</b> - Order to calculate the distance
        /// 
        /// \return A keyValuePair with the distance and time between those two cities
        /// 
        public void CalculateDistanceAndTime(Trip trip)
        {
            City origin = trip.OriginCity;
            City destination = trip.DestinationCity;
            JobType jb = trip.JobType;

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
                trip.TotalTime = 0.0;
                trip.TotalDistance = 0;

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
            trip.TotalTime = totalTime;
            trip.TotalDistance = totalDistance;
        }
    }
}

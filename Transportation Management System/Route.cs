using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    public class Route
    {
        /// The destination city
        public City Destination { set; get; }

        /// The total distance in KM
        public int Distance { set; get; }

        /// The time it takes from one citu to another
        public double Time { set; get; }

        /// The city that is located to the West of destination
        public City West { set; get; }

        /// "Pointer" to the city on west
        public Route WestPtr { set; get; }

        /// The city that is located to the East of destination
        public City East { set; get; }

        // "Pointer" to the city on East
        public Route EastPtr { set; get; }


        public Route() { }

        public Route(City newDestination, int newDistance, double newTime, City newWest, City newEast)
        {
            Destination = newDestination;
            Distance = newDistance;
            Time = newTime;
            West = newWest;
            East = newEast;
        }



        ///
        /// \brief Used to calculate the total distance and time between two cities based on the routes table
        ///
        /// \param order  - <b>Order</b> - Order to calculate the distance
        /// 
        /// \return A keyValuePair with the distance and time between those two cities
        /// 
        public KeyValuePair<int, double> CalculateDistanceAndTime(Order order)
        {
            City origin = order.Origin;
            City destination = order.Destination;
            JobType jb = order.JobType;

            int totalDistance = 0;
            double totalTime = 0.0;
            double dailyDrivingTime = 0.0;
            double dailyTotalTime = 0.0;

            // Check if origin and destination are the same, return 0
            if(origin == destination)
            {
                return new KeyValuePair<int, double>(0, 0.0);
            }

            DAL db = new DAL();
            List<Route> routes = db.GetRoutes();

            // Get the current city
            Route curr = routes[(int) origin];
            City lastCity = City.Null;

            // Do the calculation while we're didn't pass destination city or at the end of the route table
            do
            {
                totalDistance += curr.Distance;

                // Driving..
                dailyDrivingTime += curr.Time;

                // If in origin or destination or ltl + 2
                if (curr.Destination == origin || curr.Destination == destination || jb == JobType.LTL)
                {
                    // Load, Unload and stop time
                    dailyTotalTime += 2;
                }


                // Add total time driven to total time worked
                dailyTotalTime += dailyDrivingTime;

                // If the total performing hours surpassed 8 hours
                if (dailyTotalTime > 8)
                {
                    double overworkedHours;
                    double nextDayHours;

                    // Check if the driver is driving more than allowed. If he is, take a break
                    if (dailyDrivingTime > 8)
                    {
                        // Get the number of hours overworked today
                        overworkedHours = dailyDrivingTime - 8;
                        // Get the number of hours that are remaining for the next day
                        nextDayHours = dailyDrivingTime - overworkedHours;

                        // Add the number of hours allowed for the day
                        totalTime += 8;

                        // Wait until next day (24-8)
                        totalTime += 16;

                        // Add the remaining hours for the next day
                        totalTime += nextDayHours;

                        // New Day, New Hours
                        dailyDrivingTime = 0;
                        dailyTotalTime = 0;
                    }
                    // The driver operated more than 12 hours
                    else if(dailyTotalTime > 12)
                    {
                        // Get the number of hours overworked
                        overworkedHours = dailyDrivingTime - 12;
                        // Get the number of hours that are remaining for the next day
                        nextDayHours = dailyTotalTime - overworkedHours;

                        // Add the number of allowed hours for the day
                        totalTime += 12;

                        // Wait until next day (24-12)
                        totalTime += 12;

                        // Add the remaining hours for the next day
                        totalTime += nextDayHours;

                        // New Day, New Hours
                        dailyDrivingTime = 0;
                        dailyTotalTime = 0;
                    }

                    // If the total time is greater than 8, but the driver hasn't driven the total 8 
                    // neither worked a total of 12 hours, just keep going to the next city
                }
                // If everything is under the limits, just keep going
                else
                {
                    totalTime += dailyTotalTime;

                    dailyDrivingTime = 0;
                    dailyTotalTime = 0;
                }

                // Going east
                if (origin < destination)
                {
                    lastCity = curr.West;
                    curr = curr.EastPtr;
                }
                // Goint west
                else if (destination < origin)
                {
                    curr = curr.WestPtr;
                    lastCity = curr.East;
                }
            } while (curr != null && lastCity != destination);
            

            return new KeyValuePair<int, double>(totalDistance, totalTime);
        }
    }
}

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
        /// \param origin  - <b>City</b> - Origin city
        /// \param destination  - <b>City</b> - Destination city
        /// 
        /// 
        /// \return A keyValuePair with the distance and time between those two cities
        /// 
        public KeyValuePair<int, double> CalculateDistanceAndTime(City origin, City destination, JobType jb)
        {
            int totalDistance = 0;
            double totalTime = 0.0;
            double dailyDrivingTime = 0.0;

            DAL db = new DAL();
            List<Route> routes = db.GetRoutes();


            // Get the current city
            Route curr = routes[(int) origin];


            // Do the calculation while we're didn't pass destination city or at the end of the route table
            // Going east
            if (origin < destination)
            {
                do
                {
                    totalDistance += curr.Distance;
                    dailyDrivingTime += curr.Time;

                    // If in origin or destination or ltl + 2
                    if(curr.Destination == origin || curr.Destination == destination || jb == JobType.LTL)
                    {
                        // Load, Unload and stop time
                        totalTime += 2;
                    }
                    
                    // The driver can only drive for 8 hours
                    if(dailyDrivingTime >= 8)
                    {
                        // Start working again next day
                        totalTime += 16;

                        // Start fresh next day
                        dailyDrivingTime = 0;
                    }


                    totalTime += dailyDrivingTime;
                    curr = curr.EastPtr;

                } while (curr != null && curr.West != destination);
            }
            // Goint west
            else if (destination < origin)
            {
                do
                {
                    totalDistance += curr.Distance;
                    dailyDrivingTime += curr.Time;

                    // If in origin or destination or ltl + 2
                    if (curr.Destination == origin || curr.Destination == destination || jb == JobType.LTL)
                    {
                        // Load, Unload and stop time
                        totalTime += 2;
                    }


                    totalTime += dailyDrivingTime;
                    curr = curr.WestPtr;


                } while (curr != null && curr.East != destination) ;
            
            }


            return new KeyValuePair<int, double>(totalDistance, totalTime);
        }
    }
}

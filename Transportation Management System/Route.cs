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
        public string Destination { set; get; }

        /// The total distance in KM
        public int Distance { set; get; }

        /// The time it takes from one citu to another
        public double Time { set; get; }

        /// The city that is located to the West of destination
        public string West { set; get; }

        /// The city that is located to the East of destination
        public string East { set; get; }


        public Route() { }

        public Route(string newDestination, int newDistance, double newTime, string newWest, string newEast)
        {
            Destination = newDestination;
            Distance = newDistance;
            Time = newTime;
            West = newWest;
            East = newEast;
        }
                
    }
}

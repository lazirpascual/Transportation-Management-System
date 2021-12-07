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
        public int TotalDistance { get; set; }

        /// The total number of days needed for the trip
        public double TotalTime { get; set; }

        /// The JobType for the trip
        public JobType JobType { get; set; }

        /// The VanType for the trip
        public VanType VanType { get; set; }


    }
}

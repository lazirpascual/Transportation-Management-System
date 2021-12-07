namespace Transportation_Management_System
{
    /// 
    /// \class Carrier
    /// 
    /// \brief The purpose of this class is to hold and model all attributes of the carrier
    ///
    /// This class will simply keep the updated attributes of the carrier that are fetched by
    /// the CarrierManager class methods.
    /// 
    ///
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Carrier
    {
        /// The ID of the carrier company
        public long CarrierID { set; get; }

        /// The name of the carrier company
        public string Name { set; get; }

        /// The amount per km for full truckload
        public double FTLRate { set; get; }

        /// The amount per pallet per km for less than truckloads
        public double LTLRate { set; get; }

        /// The amount for reefer van type
        public double ReeferCharge { set; get; }


        public Carrier(string newName, double newFTL, double newLTL, double newReefer)
        {
            Name = newName;
            FTLRate = newFTL;
            LTLRate = newLTL;
            ReeferCharge = newReefer;
        }

        public Carrier() { }


        public bool IsActive { set; get; }


    }
}

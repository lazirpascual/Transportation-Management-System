namespace Transportation_Management_System
{
    public class CarrierCity
    {
        // Carrier of that city
        public Carrier Carrier { set; get; }

        /// The city of the carrier company
        public City DepotCity { set; get; }

        /// The number of full truckloads available
        public int FTLAval { set; get; }

        /// The number of less than truckloads available
        public int LTLAval { set; get; }

        public CarrierCity() { }

        public CarrierCity(Carrier newCarrier, City newDepot, int newFTL, int newLTL)
        {
            Carrier = newCarrier;
            DepotCity = newDepot;
            FTLAval = newFTL;
            LTLAval = newLTL;
        }
    }
}

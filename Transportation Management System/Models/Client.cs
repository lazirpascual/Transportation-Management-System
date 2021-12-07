namespace Transportation_Management_System
{

    /// 
    /// \class Client
    /// 
    /// \brief The purpose of this class is to represent a Client object
    ///
    /// A Client will be specified in the contract, fetched from the contract marketplace
    ///
    /// \author <i>Team Blank</i>
    ///
    public class Client
    {
        /// The ID of the client
        public int ClientID { get; set; }

        /// The client full name
        public string ClientName { get; set; }

        /// used to describe whether the client is active or not
        public int IsActive { get; set; }

        /// Create client Constructor
        public Client(string name)
        {
            ClientName = name;
        }

        public Client() { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Transportation_Management_System
{
    /// 
    /// \class DAL
    /// 
    /// \brief The purpose of this class is to act as the Data Access Layer for the system
    ///
    /// This class will manage all the communication between the controller to the database.
    /// Therefore, each query or change to the database need to be managed by the DAL class.
    /// 
    /// \author <i>Team Blank</i>
    ///
    class DAL : Authentication
    {
        private string Server { get; set; }         /// Host of the database
        private string User { get; set; }           /// Username to connect to the database
        private string Port { get; set; }           /// Port to connect to the database
        private string Password { get; set; }       /// Port to connect to the database
        private string DatabaseName { get; set; }   /// Name of the database

        public DAL()
        {
            Server = "phtfaw4p6a970uc0.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            User = "x4sqrh7d39h1orji;database=qbwrvu2d70g3dyis";
            Port = "3306";
            Password = "p6z1zn2nmrv396ke";
            DatabaseName = "qbwrvu2d70g3dyis";
        }


        ///
        /// \brief Returns the string connection for the database
        /// 
        /// \return String representation of the databse connection info
        /// 
        public override string ToString()
        {
            return $"server={Server};user={User};database={DatabaseName};port={Port};password={Password}";
        }



        ///
        /// \brief Inserts a new user in the User table
        ///
        /// \param usr  - <b>User</b> - An User object with all their information
        /// 
        public void CreateUser(User usr) { }



        ///
        /// \brief Inserts a new order in the Orders table
        ///
        /// \param order  - <b>Order</b> - An Order object to be created with all its information
        /// 
        public void CreateOrder(Order order) { }



        ///
        /// \brief Inserts a new client in the Client table
        ///
        /// \param client  - <b>Client</b> - A Client object to be created with all their information
        /// 
        public void CreateClient(Client client) { }



        ///
        /// \brief Inserts a new trip in the Trip table
        ///
        /// \param trip  - <b>Trip</b> - An Trip object with all its information
        /// 
        public void CreateTrip(Trip trip) { }



        ///
        /// \brief Inserts a new invoice in the Invoice table
        ///
        /// \param invoice  - <b>Invoice</b> - An Invoice object with all its information
        /// 
        public void CreateInvoice(Invoice invoice) { }



        ///
        /// \brief Inserts a new carrier in the Carrier table
        ///
        /// \param carrier  - <b>Carrier</b> - An Carrier object with all their information
        /// 
        public void CreateCarrier(Carrier carrier) { }



        ///
        /// \brief Update an existing carrier's attributes
        ///
        /// \param carrierId  - <b>int</b> - The id of the carrier to be updated
        /// \param newCarrier  - <b>Carrier</b> - The new carrier information to be used in the update
        /// 
        public void UpdateCarrier(int carrierId, Carrier newCarrier) { }



        ///
        /// \brief Get a carrier by its id
        ///
        /// \param carrierId  - <b>int</b> - The id of the carrier to be searched
        /// 
        /// \return The found carrier or null otherwise
        /// 
        public Carrier GetCarrier(int carrierId) { }



        ///
        /// \brief Deactivate an active carrier by its id
        ///
        /// \param carrier  - <b>Carrier</b> - The id of the carrier to be deactived
        /// 
        public void DeactivateCarrier(int carrierId) { }



        ///
        /// \brief Filter carriers by city
        ///
        /// \param city  - <b>string</b> - The city to be filter the carriers
        /// 
        /// \return A list of carriers that belong to the specified city
        /// 
        public List<Carrier> FilterCarrierByCity(string city) { }




        ///
        /// \brief Returns a list of all users in our system
        /// 
        /// \return A list of all registered uses
        /// 
        public List<User> GetUsers() { }


        ///
        /// \brief Returns a list of all clients in our system
        /// 
        /// \return A list of all clients
        /// 
        public List<Client> GetClient() { }



        ///
        /// \brief Filter clients by Name
        ///
        /// \param name  - <b>string</b> - The name of the client to be searched
        /// 
        /// \return The found client of null if none are found
        /// 
        public Client FilterClientByName(string name) { }


        ///
        /// \brief Returns a list of all active orders
        /// 
        /// \return List of all active orders
        /// 
        public List<Order> GetActiveOrders() { }


        ///
        /// \brief Returns a list with all trips attached to a specific orders
        /// 
        /// \param orderId  - <b>int</b> - If of the order to filter the trip
        /// 
        /// \return A list with all trips attached to a specific orders
        /// 
        public List<Trip> FilterTripsByOrderId(int orderId) { }



        ///
        /// \brief Backup up the entire database to a .sql file
        /// 
        /// \return True if successful, false otherwise
        /// 
        public bool BackupDatabase() 
        { 
            https://stackoverflow.com/questions/12311492/backing-up-database-in-mysql-using-c-sharp/12311685
        }


    }
}

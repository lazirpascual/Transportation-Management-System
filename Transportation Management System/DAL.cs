using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Transportation_Management_System
{
    class DAL : Authentication
    {
        private MySqlConnection Connection { get; set; }
        private MySqlTransaction Transaction { get; set; }
        private string Server { get; set; }
        private string User { get; set; }
        private string Port { get; set; }
        private string Password { get; set; }
        private string DatabaseName { get; set; }

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

    }
}

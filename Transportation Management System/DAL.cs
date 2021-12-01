﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
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
    public class DAL
    {
        private string Server { get; set; }         /// Host of the database
        private string User { get; set; }           /// Username to connect to the database
        private string Port { get; set; }           /// Port to connect to the database
        private string Password { get; set; }       /// Port to connect to the database
        private string DatabaseName { get; set; }   /// Name of the database

        ///
        /// \brief Contruct the DataAccessLayer object by setting the database connection information
        /// 
        public DAL()
        {
            Server = "phtfaw4p6a970uc0.cbetxkdyhwsb.us-east-1.rds.amazonaws.com";
            User = "x4sqrh7d39h1orji";
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
        /// \brief Check if the username exists in the User tables
        ///
        /// \param userName  - <b>string</b> - Username of ther user
        /// 
        /// \return True if it exists, false otherwise
        ///
        public bool CheckUsername(string userName)
        {
            bool existent = false;

            DAL db = new DAL();

            using (MySqlConnection conn = new MySqlConnection(db.ToString()))
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                try
                {
                    string sql = $"SELECT * FROM Users WHERE Username='{userName}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    // If data is found
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string DbUsername = rdr[2].ToString();
                            if (userName == DbUsername)
                            {
                                existent = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }

            return existent;
        }



        ///
        /// \brief Check if the user password is valid
        ///
        /// \param userName  - <b>string</b> - Username to check the password
        /// \param password  - <b>string</b> - Password to be validated
        /// 
        /// \return True if the password is correct, false otherwise
        ///
        public bool CheckUserPassword(string userName, string password)
        {
            // Compare Hased password
            bool isValid = false;

            DAL db = new DAL();

            using (MySqlConnection conn = new MySqlConnection(db.ToString()))
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                try
                {
                    string sql = $"SELECT * FROM Users WHERE Username='{userName}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    // If data is found
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string DbPassword = rdr[3].ToString();
                            if (BC.Verify(password, DbPassword))
                            {
                                isValid = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }

            return isValid;
        }



        ///
        /// \brief Check if the user belongs to the role specified
        ///
        /// \param type  - <b>string</b> - User role to be validated
        /// \param username  - <b>string</b> - Username to be checked
        /// 
        /// \return True if the user belongs to the specified type/role, false otherwise
        ///
        public string GetUserType(string username)
        {
            string userType = null;

            DAL db = new DAL();

            using (MySqlConnection conn = new MySqlConnection(db.ToString()))
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                try
                {
                    string sql = $"SELECT UserType FROM Users WHERE Username='{username}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    // If data is found
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string DbUserType = rdr[0].ToString();
                            userType = DbUserType;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                conn.Close();
            }

            return userType;
        }


        ///
        /// \brief Inserts a new user in the User table
        ///
        /// \param usr  - <b>User</b> - An User object with all their information
        /// 
        public void CreateUser(User usr)
        {
            //// To Test ////
            string sql = "INSERT INTO User (FirstName, LastName, Username, Password, Email, IsActive, UserType) " +
                "VALUES (@FirstName, @LastName, @Username, @Password, @Email, @IsActive, @UserType)";

            DAL db = new DAL();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(db.ToString()))
                {
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", usr.LastName);
                        cmd.Parameters.AddWithValue("@Username", usr.Username);
                        cmd.Parameters.AddWithValue("@Password", usr.Password);
                        cmd.Parameters.AddWithValue("@Email", usr.Email);
                        cmd.Parameters.AddWithValue("@IsActive", usr.IsActive);
                        cmd.Parameters.AddWithValue("@UserType", usr.UserType);

                        // Execute the insertion and check the number of rows affected
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            throw new Exception($"User {usr.Username} already exists.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
            }

        }

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
        //public Carrier GetCarrier(int carrierId) { }



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
        //public List<Carrier> FilterCarrierByCity(string city) { }




        ///
        /// \brief Returns a list of all users in our system
        /// 
        /// \return A list of all registered uses
        /// 
        //public List<User> GetUsers() { }


        ///
        /// \brief Returns a list of all clients in our system
        /// 
        /// \return A list of all clients
        /// 
        //public List<Client> GetClient() { }



        ///
        /// \brief Filter clients by Name
        ///
        /// \param name  - <b>string</b> - The name of the client to be searched
        /// 
        /// \return The found client of null if none are found
        /// 
        //public Client FilterClientByName(string name) { }


        ///
        /// \brief Returns a list of all active orders
        /// 
        /// \return List of all active orders
        /// 
        //public List<Order> GetActiveOrders() { }


        ///
        /// \brief Returns a list with all trips attached to a specific orders
        /// 
        /// \param orderId  - <b>int</b> - If of the order to filter the trip
        /// 
        /// \return A list with all trips attached to a specific orders
        /// 
        //public List<Trip> FilterTripsByOrderId(int orderId) { }



        ///
        /// \brief Backup up the entire database to a .sql file
        /// 
        /// \return True if successful, false otherwise
        /// 
        //public bool BackupDatabase() 
        //{ 
        //    // https://stackoverflow.com/questions/12311492/backing-up-database-in-mysql-using-c-sharp/12311685
        //}


    }
}

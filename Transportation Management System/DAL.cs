using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using System.Configuration;

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
            PopulateConnectionString();
        }


        ///
        /// \brief Pupulate the fields for the database connection
        /// 
        private void PopulateConnectionString()
        {
            try
            {
                Server = ConfigurationManager.AppSettings.Get("Server");
                User = ConfigurationManager.AppSettings.Get("User");
                Port = ConfigurationManager.AppSettings.Get("Port");
                Password = ConfigurationManager.AppSettings.Get("Password");
                DatabaseName = ConfigurationManager.AppSettings.Get("DatabaseName");

                if(Server == "" || User == "" || Port == "" || Password == "" || DatabaseName == "")
                {
                    throw new Exception("We couldn't retrieve the information about the database. Please check your config file.");
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Critical);
                throw;
            }
            
        }


        ///
        /// \brief Update the information about the database connection string in the config file
        ///
        /// \param fieldToChange  - <b>string</b> - field to change
        /// \param newData  - <b>string</b> - new information
        /// 
        public void UpdateDatabaseConnectionString(string fieldToChange, string newData)
        {
            List<string> availableFields = new List<string>()
            { "Server" , "User" ,"Port", "Password", "DatabaseName"};

            if(!availableFields.Contains(fieldToChange))
            {
                throw new KeyNotFoundException($"Error. We couldn't find any field called {fieldToChange} in the database connection string.");
            }

            string oldData = ConfigurationManager.AppSettings.Get(fieldToChange);

            // If the directory doesn't change, don't don anything
            if (oldData == newData) return;

            // Update the config file
            Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            configuration.AppSettings.Settings[fieldToChange].Value = newData;
            configuration.Save(ConfigurationSaveMode.Full, true);
            ConfigurationManager.RefreshSection("appSettings");

            Logger.Log($"{fieldToChange} database connection string changed from {oldData} to {newData}", LogLevel.Information);

            PopulateConnectionString();
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

            using (MySqlConnection conn = new MySqlConnection(this.ToString()))
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
                            string DbUsername = rdr["Username"].ToString();
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

            using (MySqlConnection conn = new MySqlConnection(this.ToString()))
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
                            string DbPassword = rdr["PasswordHash"].ToString();
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


            using (MySqlConnection conn = new MySqlConnection(this.ToString()))
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
                            UserRole DbUserType = (UserRole)Int32.Parse(rdr["UserType"].ToString());
                            userType = DbUserType.ToString();
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
            string sql = "INSERT INTO Users (FirstName, LastName, Username, PasswordHash, Email, IsActive, UserType) " +
                "VALUES (@FirstName, @LastName, @Username, @Password, @Email, @IsActive, @UserType)";

           
            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@FirstName", usr.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", usr.LastName);
                        cmd.Parameters.AddWithValue("@Username", usr.Username);
                        cmd.Parameters.AddWithValue("@Password", Helper.HashPass(usr.Password));
                        cmd.Parameters.AddWithValue("@Email", usr.Email);
                        cmd.Parameters.AddWithValue("@IsActive", usr.IsActive);
                        cmd.Parameters.AddWithValue("@UserType", (int) usr.UserType);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"User \"{usr.Username}\" already exists.");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }

        }



        ///
        /// \brief Inserts a new order in the Orders table
        ///
        /// \param order  - <b>Order</b> - An Order object to be created with all its information
        /// 
        public void CreateOrder(Order order) 
        {
            string sql = "INSERT INTO Orders (ClientID, OrderDate, Origin, Destination, JobType, Quantity, VanType) " +
                "VALUES (@ClientID, @OrderDate, @Origin, @Destination, @JobType, @Quantity, @VanType)";


            try
            {
                // Get the client from the database and raise an error if it doesn't exist
                Client client;
                if ((client = this.FilterClientByName(order.ClientName)) == null)
                {
                    throw new KeyNotFoundException($"Client {order.ClientName} does not exist in the database.");
                }

                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@ClientID", client.ClientID);
                        cmd.Parameters.AddWithValue("@OrderDate", order.OrderCreationDate.ToString("yyyy-MM-dd H:mm:ss"));
                        cmd.Parameters.AddWithValue("@Origin", order.Origin.ToString());
                        cmd.Parameters.AddWithValue("@Destination", order.Destination.ToString());
                        cmd.Parameters.AddWithValue("@JobType", order.JobType);
                        cmd.Parameters.AddWithValue("@Quantity", order.Quantity);
                        cmd.Parameters.AddWithValue("@VanType", order.VanType);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Something went wrong while creating the order. {e.Message}");
            }
            catch (KeyNotFoundException e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }



        ///
        /// \brief Inserts a new client in the Client table
        ///
        /// \param client  - <b>Client</b> - A Client object to be created with all their information
        /// 
        public void CreateClient(Client client) 
        {
            string sql = "INSERT INTO Clients (ClientName) VALUES (@ClientName)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@ClientName", client.ClientName);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Client {client.ClientName} already exists.");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }



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
        public long CreateCarrier(Carrier carrier) 
        {
            string sql = "INSERT INTO Carriers (CarrierName, FTLRate, LTLRate, reefCharge) VALUES (@CarrierName, @FTLRate, @LTLRate, @reefCharge)";

            long id;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierName", carrier.Name);
                        cmd.Parameters.AddWithValue("@FTLRate", carrier.FTLRate);
                        cmd.Parameters.AddWithValue("@LTLRate", carrier.LTLRate);
                        cmd.Parameters.AddWithValue("@reefCharge", carrier.ReeferCharge);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();

                        id = cmd.LastInsertedId;
                    }
                }
            }
            catch (MySqlException e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Carrier \"{carrier.Name}\" already exists.");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }

            return id;       // Get the ID of the inserted item
        }


        ///
        /// \brief Inserts a new carrier in the Carrier table
        ///
        /// \param carrier  - <b>Carrier</b> - An Carrier object with all their information
        /// 
        public void CreateCarrierCity(CarrierCity carrierCity)
        {
            string sql = "INSERT INTO CarrierCity (CarrierID, DepotCity, FTLAval, LTLAval) VALUES (@CarrierID, @DepotCity, @FTLAval, @LTLAval)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierID", carrierCity.Carrier.CarrierID);
                        cmd.Parameters.AddWithValue("@DepotCity", carrierCity.DepotCity.ToString());
                        cmd.Parameters.AddWithValue("@FTLAval", carrierCity.FTLAval);
                        cmd.Parameters.AddWithValue("@LTLAval", carrierCity.LTLAval);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Carrier Depot city \"{carrierCity.DepotCity.ToString()}\" already exists.");
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }


        ///
        /// \brief Update an existing carrier's attributes
        ///
        /// \param newCarrier  - <b>Carrier</b> - The new carrier information to be used in the update
        /// 
        public void UpdateCarrier(Carrier newCarrier) 
        {
            string sql = "UPDATE Carriers SET CarrierName=@CarrierName, FTLRate=@FTLRate, LTLRate=@LTLRate, ReefCharge=ReefCharge WHERE CarrierID=@CarrierID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierName", newCarrier.Name);
                        cmd.Parameters.AddWithValue("@FTLRate", newCarrier.FTLRate);
                        cmd.Parameters.AddWithValue("@LTLRate", newCarrier.LTLRate);
                        cmd.Parameters.AddWithValue("@ReefCharge", newCarrier.ReeferCharge);
                        cmd.Parameters.AddWithValue("@CarrierID", newCarrier.CarrierID);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }


        ///
        /// \brief Update an existing carrier's attributes
        ///
        /// \param newCarrier  - <b>Carrier</b> - The new carrier information to be used in the update
        /// 
        public void UpdateCarrierCity(CarrierCity newCarrierCity)
        {
            string sql = "UPDATE CarrierCity SET DepotCity=@DepotCity, FLTAval=@FLTAval, LTLAval=@LTLAval WHERE CarrierID=@CarrierID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierID", newCarrierCity.Carrier.CarrierID);
                        cmd.Parameters.AddWithValue("@DepotCity", newCarrierCity.DepotCity.ToString());
                        cmd.Parameters.AddWithValue("@FLTAval", newCarrierCity.FTLAval);
                        cmd.Parameters.AddWithValue("@LTLAval", newCarrierCity.LTLAval);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }



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
        /// \param carrier  - <b>Carrier</b> - The new carrier information to be used in the deactivation
        /// 
        public void DeactivateCarrier(Carrier carrier) 
        {
            string sql = "UPDATE Carriers SET IsActive=0 WHERE CarrierID=@CarrierID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierID", carrier.CarrierID);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }


        ///
        /// \brief Deactivate an active carrier by its id
        ///
        /// \param carrier  - <b>Carrier</b> - The new carrier information to be used in the deactivation
        /// 
        public void DeleteCarrier(Carrier carrier)
        {
            string sql = "DELETE FROM Carriers WHERE CarrierID=@CarrierID";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierID", carrier.CarrierID);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }


        ///
        /// \brief Remove a city from the carrier
        ///
        /// \param carrierCity  - <b>CarrierCity</b> - The city to be deleted
        /// 
        public void RemoveCarrierCity(CarrierCity carrierCity)
        {
            string sql = "DELETE FROM CarrierCity INNER JOIN Carriers ON CarrierCity.CarrierID = Carriers.CarrierID WHERE CarrierID=@CarrierID AND DepotCity=@DepotCity";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@CarrierID", carrierCity.Carrier.CarrierID);
                        cmd.Parameters.AddWithValue("@DepotCity", carrierCity.DepotCity);

                        // Execute the insertion and check the number of rows affected
                        // An exception will be thrown if the column is repeated
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw;
            }
        }



        ///
        /// \brief Filter carriers by city
        ///
        /// \param city  - <b>string</b> - The city to be filter the carriers
        /// 
        /// \return A list of carriers that belong to the specified city
        /// 
        public List<Carrier> FilterCarriersByCity(City city)
        {
            List<Carrier> carriers = new List<Carrier>();
            string qSQL = "SELECT * FROM Carriers INNER JOIN CarrierCity ON CarrierCity.CarrierID = Carriers.CarrierID WHERE DepotCity=@DepotCity AND IsActive=1";

            try
            {
                string conString = this.ToString();
                using (MySqlConnection conn = new MySqlConnection(conString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(qSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@DepotCity", city.ToString());
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Carrier carr = new Carrier();
                                carr.CarrierID = int.Parse(rdr["CarrierID"].ToString());
                                carr.Name = rdr["CarrierName"].ToString();
                                carr.FTLRate = double.Parse(rdr["FTLRate"].ToString());
                                carr.LTLRate = double.Parse(rdr["LTLRate"].ToString());
                                carr.ReeferCharge = double.Parse(rdr["reefCharge"].ToString());
                                carriers.Add(carr);

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return carriers;
        }


        ///
        /// \brief Filter carriers by city
        ///
        /// \param city  - <b>string</b> - The city to be filter the carriers
        /// 
        /// \return A list of carriers that belong to the specified city
        /// 
        public List<CarrierCity> FilterCitiesByCarrier(string carrierName)
        {
            List<CarrierCity> carrierCities = new List<CarrierCity>();
            string qSQL = "SELECT * FROM CarrierCity INNER JOIN Carriers ON CarrierCity.CarrierID = Carriers.CarrierID WHERE CarrierName=@CarrierName AND IsActive=1";

            try
            {
                string conString = this.ToString();
                using (MySqlConnection conn = new MySqlConnection(conString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(qSQL, conn))
                    {
                        cmd.Parameters.AddWithValue("@CarrierName", carrierName);

                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                // Getting the carrier of that city
                                Carrier carr = new Carrier();
                                carr.CarrierID = int.Parse(rdr["CarrierID"].ToString());
                                carr.Name = rdr["CarrierName"].ToString();
                                carr.FTLRate = double.Parse(rdr["FTLRate"].ToString());
                                carr.LTLRate = double.Parse(rdr["LTLRate"].ToString());
                                carr.ReeferCharge = double.Parse(rdr["reefCharge"].ToString());


                                CarrierCity carrCity = new CarrierCity();
                                carrCity.Carrier = carr;
                                carrCity.DepotCity = (City)Enum.Parse(typeof(City), rdr["DepotCity"].ToString(), true);
                                carrCity.FTLAval = int.Parse(rdr["FTLAval"].ToString());
                                carrCity.LTLAval = int.Parse(rdr["LTLAval"].ToString());


                                carrierCities.Add(carrCity);

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return carrierCities;
        }



        ///
        /// \brief Returns a list of all users in our system
        /// 
        /// \return A list of all registered uses
        /// 
        public List<User> GetUsers() 
        {
            List<User> usersList = new List<User>();
            string qSQL = "SELECT * FROM Users";
            try
            {
                string connectionString = this.ToString();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(qSQL, conn))
                    {
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                User user = new User();
                                user.FirstName = rdr["FirstName"].ToString();
                                user.LastName = rdr["LastName"].ToString();
                                user.Username = rdr["Username"].ToString();
                                user.Password = rdr["PasswordHash"].ToString();
                                user.Email = rdr["Email"].ToString();
                                user.IsActive = Boolean.Parse(rdr["IsActive"].ToString());
                                user.UserType = (UserRole)int.Parse(rdr["UserType"].ToString());
                                usersList.Add(user);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return usersList;
        }


        ///
        /// \brief Returns a list of all clients in our system
        /// 
        /// \return A list of all clients
        /// 
        public List<Client> GetClients()
        {
            List<Client> clientsList = new List<Client>();
            string qSQL = "SELECT * FROM Clients";
            try
            {
                string connectionString = this.ToString();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(qSQL, conn))
                    {
                        MySqlDataReader rdr = cmd.ExecuteReader();
                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                Client client = new Client();
                                client.ClientID = int.Parse(rdr["ClientID"].ToString());
                                client.ClientName = rdr["ClientName"].ToString();
                                clientsList.Add(client);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return clientsList;
        }



        ///
        /// \brief Filter clients by Name
        ///
        /// \param name  - <b>string</b> - The name of the client to be searched
        /// 
        /// \return The found client of null if none are found
        /// 
        public Client FilterClientByName(string name) 
        {
            string sql = "SELECT ClientID, ClientName FROM Clients WHERE ClientName=@ClientName";
            Client client = null;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(this.ToString()))
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        // Populate all arguments in the insert
                        cmd.Parameters.AddWithValue("@ClientName", name);

                        MySqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                client = new Client();
                                client.ClientID = int.Parse(rdr["ClientID"].ToString());
                                client.ClientName = rdr["ClientName"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Unable to filter the clients by name. {e.Message}");
            }

            return client;
        }



        ///
        /// \brief Returns a list of all active orders
        /// 
        /// \return List of all active orders
        /// 
        public List<Order> GetActiveOrders()
        {
            List<Order> orders = new List<Order>();
            try
            {
                string conString = this.ToString();
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT Clients.ClientName, OrderDate, Origin, Destination, JobType, VanType, Quantity FROM Orders " +
                         "INNER JOIN Clients ON Orders.ClientID = Clients.ClientID WHERE IsCompleted=0", con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Order newOrder = new Order();
                            newOrder.ClientName = rdr["ClientName"].ToString();
                            newOrder.OrderCreationDate = DateTime.Parse(rdr["OrderDate"].ToString());
                            newOrder.Origin = (City) Enum.Parse(typeof(City), rdr["Origin"].ToString(), true);
                            newOrder.Destination = (City) Enum.Parse(typeof(City), rdr["Destination"].ToString(), true);
                            newOrder.JobType = (JobType) int.Parse(rdr["JobType"].ToString());
                            newOrder.VanType = (VanType) int.Parse(rdr["VanType"].ToString());
                            newOrder.Quantity = int.Parse(rdr["Quantity"].ToString());
                            newOrder.IsCompleted = 0;
                            orders.Add(newOrder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Unable to fetch all active orders. {e.Message}");
            }

            return orders;
        }


        ///
        /// \brief Returns a list of all orders
        /// 
        /// \return List of all orders
        /// 
        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            try
            {
                string conString = this.ToString();
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT Clients.ClientName, OrderDate, Origin, Destination, JobType, VanType, Quantity, IsCompleted FROM Orders" +
                         " INNER JOIN Clients ON Orders.ClientID = Clients.ClientID", con);

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Order newOrder = new Order();
                            newOrder.ClientName = rdr["ClientName"].ToString();
                            newOrder.OrderCreationDate = DateTime.Parse(rdr["OrderDate"].ToString());
                            newOrder.Origin = (City)Enum.Parse(typeof(City), rdr["Origin"].ToString(), true);
                            newOrder.Destination = (City)Enum.Parse(typeof(City), rdr["Destination"].ToString(), true);
                            newOrder.JobType = (JobType)int.Parse(rdr["JobType"].ToString());
                            newOrder.VanType = (VanType)int.Parse(rdr["VanType"].ToString());
                            newOrder.Quantity = int.Parse(rdr["Quantity"].ToString());
                            newOrder.IsCompleted = int.Parse(rdr["IsCompleted"].ToString());
                            orders.Add(newOrder);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Unable to fetch all orders. {e.Message}");
            }

            return orders;
        }

        ///
        /// \brief Return a list of all active customers in our system
        /// 
        /// \return A list of all active customers
        /// 
        public List<Client> GetActiveClients()
        {
            List<Client> clients = new List<Client>();
            try
            {
                string conString = this.ToString();
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT ClientName From Clients WHERE IsActive=1", con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Client newClient = new Client();
                            newClient.ClientName = rdr["ClientName"].ToString();
                            clients.Add(newClient);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message, LogLevel.Error);
                throw new ArgumentException($"Unable to fetch all active clients. {e.Message}");
            }

            return clients;
        }
    }

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


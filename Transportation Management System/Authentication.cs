using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using BC = BCrypt.Net.BCrypt;


namespace Transportation_Management_System
{
    /// 
    /// \class Authentication
    /// 
    /// \brief The purpose of this class is to authenticate an attempt to log in into the system
    ///
    /// This class represents the system authenticator that will validate a log in attempt to the system and 
    /// check if the username is valid and if the password is correct 
    /// 
    /// \author <i>Team Blank</i>
    ///
    public class Authentication
    {

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
        public bool CheckUserType(string type, string username)
        {
            bool IsTypeValid = false;

            DAL db = new DAL();

            using (MySqlConnection conn = new MySqlConnection(db.ToString()))
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                try
                {
                    string sql = $"SELECT * FROM Users WHERE UserType='{type}' AND Username='{username}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    // If data is found
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string DbUsername = rdr[2].ToString();
                            string DbUserType = rdr[5].ToString();
                            if (type == DbUserType && username == DbUsername)
                            {
                                IsTypeValid = true;
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

            return IsTypeValid;
        }



        ///
        /// \brief Generate a salted hash of the password
        ///
        /// \param password  - <b>string</b> - Password to be hashed
        /// 
        /// \return Hashed password
        ///
        public string HashPass(string password)
        {
            string mySalt = BC.GenerateSalt();
            
            return BC.HashPassword(password, mySalt);
        }
    }
}

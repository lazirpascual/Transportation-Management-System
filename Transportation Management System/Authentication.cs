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
    class Authentication
    {
        public bool CheckUsername(string userName)
        {
            bool existent = false;

            Database db = new Database();

            using (MySqlConnection conn = new MySqlConnection(db.GetConnectionStr()))
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
                            string DbUsername = rdr[1].ToString();
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
            }

            return existent;
        }



        public bool CheckPassword(string password)
        {
            // Compare Hased password
            bool isValid = false;

            Database db = new Database();

            using (MySqlConnection conn = new MySqlConnection(db.GetConnectionStr()))
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                try
                {
                    string sql = $"SELECT * FROM Users WHERE Password='{password}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    
                    // If data is found
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string DbPassword = rdr[2].ToString();
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
            }

            return isValid;
        }



        public bool CheckUserType(string type, string username)
        {
            bool IsTypeValid = false;

            Database db = new Database();

            using (MySqlConnection conn = new MySqlConnection(db.GetConnectionStr()))
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                try
                {
                    string sql = $"SELECT * FROM Users WHERE Type='{type}' AND Username='{username}'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    // If data is found
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            string DbUsername = rdr[0].ToString();
                            string DbUserType = rdr[3].ToString();
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
            }

            return IsTypeValid;
        }



        public string HashPass(string password)
        {
            BC.GenerateSalt();
            
            return BC.HashPassword(password);
        }
    }
}

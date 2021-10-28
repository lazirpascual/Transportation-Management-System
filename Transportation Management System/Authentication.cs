using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;
using BCrypt;


namespace Transportation_Management_System
{
    class Authentication
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments.
            Authentication auth = new Authentication();
            auth.ConnectToDB();
        }


        private MySqlConnection conn;

        public bool ConnectToDB()
        {
            


            

            //conn.Close();
            Console.WriteLine("Done.");

            return true;
        }

        private bool CheckUsername(string userName)
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


        private bool CheckPassword(string password)
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
                            if (password == DbPassword)
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


        private string HashPass(string password)
        {

        }
    }
}

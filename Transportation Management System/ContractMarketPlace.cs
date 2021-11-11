using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using MySql.Data;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;

namespace Transportation_Management_System
{
    class ContractMarketPlace
    {
        public string CMPServer { get; set; }
        public string DBName { get; set; }
        public string UID { get; set; }
        public string Password { get; set; }

        public ContractMarketPlace()
        {
            CMPServer = "159.89.117.198";
            DBName = "cmp";
            UID = "DevOSHT";
            Password = "Snodgr4ss!";
        }
        public string CMPConnectionString()
        {
            return $"SERVER={CMPServer};DATABASE={DBName};UID={UID};PASSWORD={Password}";
        }

        public List<Contract> GetContracts()
        {
            List<Contract> contracts = new List<Contract>();
            try
            {
                string conString = CMPConnectionString();
                using (MySqlConnection con = new MySqlConnection(conString))
                {
                    MySqlCommand cmd = new MySqlCommand("SELECT * FROM Contract", con);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            Contract cons = new Contract();
                            cons.Client = rdr["CLIENT_NAME"].ToString();
                            cons.JobType = int.Parse(rdr["JOB_TYPE"].ToString());
                            cons.Quantity = int.Parse(rdr["QUANTITY"].ToString());
                            cons.Origin = rdr["ORIGIN"].ToString();
                            cons.Destination = rdr["DESTINATION"].ToString();
                            cons.VanType = int.Parse(rdr["VAN_TYPE"].ToString());
                            contracts.Add(cons);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return contracts;
        }
    }
}

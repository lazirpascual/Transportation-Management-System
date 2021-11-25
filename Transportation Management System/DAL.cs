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

        public string GetConnectionStr()
        {
            return $"server={Server};user={User};database={DatabaseName};port={Port};password={Password}";

        }



        public void CreateUser(User usr)
        {

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Transportation_Management_System
{
    class Database
    {
        private MySqlConnection Connection { get; set; }
        private MySqlTransaction Transaction { get; set; }
        public string Server { get; set; }
        public string User { get; set; }
        public string Port { get; set; }
        public string Password { get; set; }
        public string DatabaseName { get; set; }

        public Database()
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


    }
}

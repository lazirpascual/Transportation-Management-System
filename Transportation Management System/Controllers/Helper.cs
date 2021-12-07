//using MySql.Data;
using BC = BCrypt.Net.BCrypt;

namespace Transportation_Management_System
{
    /// 
    /// \class Helper
    /// 
    /// \brief The purpose of this class is to store the helper methods to our solution
    ///
    /// This class can be used to store any helper method that might support a helper functionality 
    ///
    /// \author <i>Team Blank</i>
    ///
    public static class Helper
    {

        ///
        /// \brief Generate a salted hash of the password
        ///
        /// \param password  - <b>string</b> - Password to be hashed
        /// 
        /// \return Hashed password
        ///
        public static string HashPass(string password)
        {
            string mySalt = BC.GenerateSalt();

            return BC.HashPassword(password, mySalt);
        }

    }
}

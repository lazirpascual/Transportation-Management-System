using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transportation_Management_System
{
    /// 
    /// \class User
    /// 
    /// \brief The purpose of this class is to represent the role of a user
    ///
    /// This class represents the role of a user when they login to the TMS application.
    /// There are three different types of users that will inherit from User; Buyer, Planner, Admin.
    ///
    /// \author <i>Team Blank</i>
    ///
    class User
    {
        public string FirstName { get; set; }   /// used to describe the first name of the user
        public int Username { get; set; }   /// used to represent the username of the user
        public int Password { get; set; }   /// used to represent the password of the user
        public bool IsActive { get; set; }  /// used to describe whether the user is active or not
        public string UserType { get; set; }    /// used to represent type or role of a user
    }
}

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
        public string FirstName { get; set; }
        public int Username { get; set; }
        public int Password { get; set; }
        public bool IsActive { get; set; }
        public string UserType { get; set; }
    }
}

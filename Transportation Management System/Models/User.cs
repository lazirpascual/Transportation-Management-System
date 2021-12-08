/* -- FILEHEADER COMMENT --
    FILE		:	User.cs
    PROJECT		:	Transportation Management System
    PROGRAMMER	:  * Ana De Oliveira
                   * Icaro Ryan Oliveira Souza
                   * Lazir Pascual
                   * Rohullah Noory
    DATE		:	2021-12-07
    DESCRIPTION	:	This file contains the source for the User class.
*/

namespace Transportation_Management_System
{
    /// <summary>
    /// Enum to represent user types as a number.
    /// </summary>
    public enum UserRole
    {
        Buyer,
        Planner,
        Admin
    }

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
    public class User
    {
        /// used to describe the first name of the user
        public string FirstName { get; set; }

        /// used to describe the last name of the user
        public string LastName { get; set; }

        /// used to represent the username of the user
        public string Username { get; set; }

        /// used to represent the password of the user
        public string Password { get; set; }

        /// used to represent the email of the user
        public string Email { get; set; }

        /// used to describe whether the user is active or not
        public bool IsActive { get; set; }

        /// used to represent type or role of a user
        public UserRole UserType { get; set; }

        ///
        /// \brief This User class constructor is used to initialize properties of User class.
        ///
        public User(string firstName, string lastName, string username, string password, string email, UserRole userType)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Password = password;
            Email = email;
            IsActive = true;
            UserType = userType;
        }

        ///
        /// \brief Empty User class constructor, used to access the class.
        ///
        public User()
        { }
    }
}
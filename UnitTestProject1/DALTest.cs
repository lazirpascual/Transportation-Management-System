using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class DALTest
    {
        // Test if a new user is successfully created
        [TestMethod]
        public void CreateUserNormalTest()
        {
            User usr = new User();
        }

        // Test if an alert message is displayed when inserting an existent user
        [TestMethod]
        public void CreateUserException()
        {

        }
    }
}

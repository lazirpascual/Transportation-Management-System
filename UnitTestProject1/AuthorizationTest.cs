using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Transportation_Management_System;

namespace UnitTestProject1
{
    [TestClass]
    public class AuthorizationTest
    {
        [TestMethod]
        public void CheckUsername_FunctionalTest()
        {
            string validUsername = "admin";
            var auth = new DAL();

            bool usernameResult = auth.CheckUsername(validUsername);
            Assert.AreEqual(true, usernameResult);
        }

        [TestMethod]
        public void CheckUsername_ExceptionTest()
        {
            string invalidUsername = "invalidAdmin";
            var auth = new DAL();

            var usernameResult = auth.CheckUsername(invalidUsername);
            Assert.AreEqual(false, usernameResult);
        }

        [TestMethod]
        public void CheckPassword_FunctionalTest()
        {
            string validUsername = "admin";
            string validPassword = "admin";
            var auth = new DAL();

            bool passwordResult = auth.CheckUserPassword(validUsername, validPassword);
            Assert.AreEqual(true, passwordResult);
        }

        [TestMethod]
        public void CheckPassword_ExceptionTest()
        {
            string validUsername = "admin";
            string validPassword = "invalidAdmin";
            var auth = new DAL();

            bool passwordResult = auth.CheckUserPassword(validUsername, validPassword);
            Assert.AreEqual(false, passwordResult);
        }

        [TestMethod]
        public void CheckUserType_FunctionalTest()
        {
            string validUsername = "buyer";
            var auth = new DAL();

            string usertypeResult = auth.GetUserType(validUsername);
            Assert.AreEqual("Buyer", usertypeResult);
        }

        [TestMethod]
        public void CheckUserType_ExceptionTest()
        {
            string invalidUsername = "invalidBuyer";
            var auth = new DAL();

            string usertypeResult = auth.GetUserType(invalidUsername);
            Assert.AreEqual(null, usertypeResult);
        }
    }
}

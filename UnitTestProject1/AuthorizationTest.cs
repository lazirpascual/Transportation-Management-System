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
            var auth = new Authentication();

            bool usernameResult = auth.CheckUsername(validUsername);
            Assert.AreEqual(true, usernameResult);
        }

        [TestMethod]
        public void CheckUsername_ExceptionTest()
        {
            string invalidUsername = "invalidAdmin";
            var auth = new Authentication();

            var usernameResult = auth.CheckUsername(invalidUsername);
            Assert.AreEqual(false, usernameResult);
        }

        [TestMethod]
        public void CheckPassword_FunctionalTest()
        {
            string validUsername = "admin";
            string validPassword = "admin";
            var auth = new Authentication();

            bool passwordResult = auth.CheckUserPassword(validUsername, validPassword);
            Assert.AreEqual(true, passwordResult);
        }

        [TestMethod]
        public void CheckPassword_ExceptionTest()
        {
            string validUsername = "admin";
            string validPassword = "invalidAdmin";
            var auth = new Authentication();

            bool passwordResult = auth.CheckUserPassword(validUsername, validPassword);
            Assert.AreEqual(false, passwordResult);
        }

        [TestMethod]
        public void CheckUserType_FunctionalTest()
        {
            string validUsertype = "Buyer";
            string validUsername = "buyer";
            var auth = new Authentication();

            bool usertypeResult = auth.CheckUserType(validUsertype, validUsername);
            Assert.AreEqual(true, usertypeResult);
        }

        [TestMethod]
        public void CheckUserType_ExceptionTest()
        {
            string validUsertype = "Admin";
            string validUsername = "buyer";
            var auth = new Authentication();

            bool usertypeResult = auth.CheckUserType(validUsertype, validUsername);
            Assert.AreEqual(false, usertypeResult);
        }
    }
}

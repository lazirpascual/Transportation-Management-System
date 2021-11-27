using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Transportation_Management_System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckUsername_TestFunctional()
        {
            string validUsername = "admin";
            var auth = new Authentication();

            /* functional test */
            bool usernameResult = auth.CheckUsername(validUsername);
            Assert.AreEqual(true, usernameResult);
        }

        [TestMethod]
        public void CheckUsername_TestException()
        {
            string invalidUsername = "invalidAdmin";
            var auth = new Authentication();

            /* exception test */
            var usernameResult = auth.CheckUsername(invalidUsername);
            Assert.AreEqual(false, usernameResult);
        }
    }
}

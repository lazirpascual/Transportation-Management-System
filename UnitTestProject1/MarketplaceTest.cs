using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Transportation_Management_System;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for UnitTest2
    /// </summary>
    [TestClass]
    public class MarketplaceTest
    {
        [TestMethod]
        public void GetContracts_FunctionalTest()
        {           
            Contract firstContract = new Contract();
            firstContract.ClientName = "Space J";
            firstContract.Destination = "Toronto";
            firstContract.JobType = 0;
            firstContract.Origin = "Kingston";
            firstContract.Quantity = 0;
            firstContract.VanType = 1;

            var cmpTest = new ContractMarketPlace();
            var cmpContracts = cmpTest.GetContracts();
            Contract cmpFirstContract = cmpContracts[0];

            Assert.AreEqual(firstContract, cmpFirstContract);
        }

        [TestMethod]
        public void GetContracts_ExceptionTest()
        {
            Contract firstContract = new Contract();
            firstContract.ClientName = "Malmart";
            firstContract.Destination = "Windsor";
            firstContract.JobType = 0;
            firstContract.Origin = "Belleville";
            firstContract.Quantity = 0;
            firstContract.VanType = 0;

            var cmpTest = new ContractMarketPlace();
            var cmpContracts = cmpTest.GetContracts();
            Contract cmpFirstContract = cmpContracts[0];

            Assert.AreNotEqual(firstContract, cmpFirstContract);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            firstContract.Destination = City.Toronto;
            firstContract.JobType = 0;
            firstContract.Origin = City.Kingston;
            firstContract.Quantity = 0;
            firstContract.VanType = VanType.Reefer;

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
            firstContract.Destination = City.Windsor;
            firstContract.JobType = 0;
            firstContract.Origin = City.Belleville;
            firstContract.Quantity = 0;
            firstContract.VanType = 0;

            var cmpTest = new ContractMarketPlace();
            var cmpContracts = cmpTest.GetContracts();
            Contract cmpFirstContract = cmpContracts[0];

            Assert.AreNotEqual(firstContract, cmpFirstContract);
        }
    }
}
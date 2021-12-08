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
            Contract firstContract = new Contract
            {
                ClientName = "Space J",
                Destination = City.Toronto,
                JobType = 0,
                Origin = City.Kingston,
                Quantity = 0,
                VanType = VanType.Reefer
            };

            var cmpTest = new ContractMarketPlace();
            var cmpContracts = cmpTest.GetContracts();
            Contract cmpFirstContract = cmpContracts[0];

            Assert.AreEqual(firstContract, cmpFirstContract);
        }

        [TestMethod]
        public void GetContracts_ExceptionTest()
        {
            Contract firstContract = new Contract
            {
                ClientName = "Malmart",
                Destination = City.Windsor,
                JobType = 0,
                Origin = City.Belleville,
                Quantity = 0,
                VanType = 0
            };

            var cmpTest = new ContractMarketPlace();
            var cmpContracts = cmpTest.GetContracts();
            Contract cmpFirstContract = cmpContracts[0];

            Assert.AreNotEqual(firstContract, cmpFirstContract);
        }
    }
}
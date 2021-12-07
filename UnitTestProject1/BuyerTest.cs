using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Transportation_Management_System;

namespace UnitTestProject1
{
    /// <summary>
    /// Summary description for BuyerTest
    /// </summary>
    [TestClass]
    public class BuyerTest
    {
        [TestMethod]
        public void GenerateOrder_FunctionalTest()
        {
            // Get contract from market place
            ContractMarketPlace CMP = new ContractMarketPlace();
            List<Contract> contracts = CMP.GetContracts();
            Contract contract = contracts[1];

            // Generate order
            Buyer buyer = new Buyer();
            buyer.GenerateOrder(contract);
        }

        [TestMethod]
        public void CreateOrder_ExceptionTest()
        {
        }

        [TestMethod]
        public void GetActiveCustomers_FunctionalTest()
        {
        }

        [TestMethod]
        public void GetActiveCustomers_ExceptionTest()
        {
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Transportation_Management_System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class DALTest
    {
        // Test if a new user is successfully created
        [TestMethod]
        public void CreateUserNormalTest()
        {
            bool passed;
            User usr = new User("BubuFirstName", "bubuLastName", "bubuUsername", "pass", "email@gmail.com", UserRole.Buyer);

            DAL db = new DAL();
            try
            {
                db.CreateUser(usr);
                passed = true;
            }
            catch (Exception)
            {
                // Fail if an exception is thrown
                passed = false;
            }

            Assert.IsTrue(passed);
        }

        // Test if an alert message is displayed when inserting an existent user
        [TestMethod]
        public void CreateUserException()
        {
            DAL db = new DAL();
            User usr = new User("jijiFirstName", "jijiLastName", "jiji123", "pass", "email@gmail.com", UserRole.Buyer);

            // Insert the first user normaly
            db.CreateUser(usr);

            // Insert another user with the same username
            usr = new User("babaFirstName", "babaLastName", "jiji123", "pass", "email@gmail.com", UserRole.Buyer);
            Assert.ThrowsException<ArgumentException>(() => db.CreateUser(usr));

        }



        // Test if a new client is successfully created
        [TestMethod]
        public void CreateClientNormalTest()
        {
            Client client = new Client("Xixibubu");

            DAL db = new DAL();

            // Fail if an exception is thrown
            db.CreateClient(client);
        }

        // Test if an alert message is displayed when inserting an existent client
        [TestMethod]
        public void CreateClientException()
        {
            DAL db = new DAL();
            Client client = new Client("Jujuba");

            // Insert the first client normaly
            db.CreateClient(client);

            // Insert the same client again with the same username
            Assert.ThrowsException<ArgumentException>(() => db.CreateClient(client));

        }

        // Test if correct client is returned based on the name filter criteria 
        [TestMethod]
        public void FilterClientByNameNormal()
        {
            DAL db = new DAL();

            Client client = new Client("ExistentGuy");
            try
            {
                db.CreateClient(client);
            }
            // Ignore exceptions if the client already exists
            catch {}
            

            Assert.IsNotNull(db.FilterClientByName("ExistentGuy"));
        }


        // Test if null is returned when an inexistent client is found
        [TestMethod]
        public void FilterClientByNameException()
        {
            DAL db = new DAL();
            Assert.IsNull(db.FilterClientByName("InexistentGuy"));
        }


        // Test if an order is inserted in the database
        [TestMethod]
        public void CreateOrderNormal()
        {
            // Get contract from market place
            ContractMarketPlace CMP = new ContractMarketPlace();
            List<Contract> contracts = CMP.GetContracts();

            // Generate order 
            Contract contract = contracts[0];
            City origin = (City)Enum.Parse(typeof(City), contract.Origin, true);
            City destination = (City)Enum.Parse(typeof(City), contract.Destination, true);
            Order order = new Order(contract.ClientName, DateTime.Now, origin, destination, contract.JobType, contract.Quantity, contract.VanType);


            DAL db = new DAL();

            // Make sure the client exists
            Client client = new Client(order.ClientName);
            try
            {
                db.CreateClient(client);
            }
            // Ignore exceptions if the client already exists
            catch { }

            // If any exception is throw, the test will fail
            db.CreateOrder(order);
        }

        // Test if an exception if the client does not exist
        [TestMethod]
        public void CreateOrderException()
        {
            // Get contract from market place
            ContractMarketPlace CMP = new ContractMarketPlace();
            List<Contract> contracts = CMP.GetContracts();

            // Generate order 
            Contract contract = contracts[0];
            City origin = (City)Enum.Parse(typeof(City), contract.Origin, true);
            City destination = (City)Enum.Parse(typeof(City), contract.Destination, true);
            Order order = new Order("NonExistentBuddy", DateTime.Now, origin, destination, contract.JobType, contract.Quantity, contract.VanType);


            DAL db = new DAL();

            // If any exception is throw, the test will fail
            Assert.ThrowsException<KeyNotFoundException>(() => db.CreateOrder(order));
        }
    }
}

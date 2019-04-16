using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using URent.Controllers;
using System.Web.Mvc;
using NUnit;


namespace URentTests.UnitTests
{
    [TestClass]
    public class TotalPriceTests
    {
        private SUPTransactionsController getTestObject()
        {
            return new SUPTransactionsController();
        }

        [TestMethod]
        public void TotalPrice_5Days_10Dollars()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            int targetTotal = 50;

            //act
            int actualTotal = transaction.calculateTotalPrice(5, 10);

            //assert
            Assert.AreEqual(targetTotal, actualTotal);
        }

        [TestMethod]
        public void TotalPrice_8Days_12Dollars()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            int targetTotal = 96;

            //act
            int actualTotal = transaction.calculateTotalPrice(8, 12);

            //assert
            Assert.AreEqual(targetTotal, actualTotal);
        }

        [TestMethod]
        public void TotalPrice_8Days_10Dollars()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            int wrongTotal = 90;

            //act
            int actualTotal = transaction.calculateTotalPrice(8, 10);

            //assert
            Assert.AreNotEqual(wrongTotal, actualTotal);
        }

        [TestMethod]
        public void TotalPrice_2Days_18Dollars()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            int wrongTotal = 37;

            //act
            int actualTotal = transaction.calculateTotalPrice(2, 18);

            //assert
            Assert.AreNotEqual(wrongTotal, actualTotal);
        }
    }
}

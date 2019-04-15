using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using URent.Controllers;
using System.Web.Mvc;
using NUnit;

namespace URentTests.UnitTests
{
    [TestClass]
    public class CheckDateFormatTests
    {
        private SUPTransactionsController getTestObject()
        {
            return new SUPTransactionsController();
        }

        [TestMethod]
        public void CheckDateFormat_Should_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "04/25/19";
            bool areFormattedProperly = true;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }

        [TestMethod]
        public void CheckDashedDateFormat_Should_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "04-25-19";
            bool areFormattedProperly = true;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }

        [TestMethod]
        public void CheckDifferentDateFormat_Should_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "04/25/2019";
            bool areFormattedProperly = true;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }

        [TestMethod]
        public void CheckDifferentCultureDateFormat_ShouldNot_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "25/04/19";
            bool areFormattedProperly = false;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }

        [TestMethod]
        public void CheckDifferentCultureSpelledDateFormat_Should_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "25 April 2019";
            bool areFormattedProperly = true;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }

        [TestMethod]
        public void CheckDifferentCultureDateFormatStartingWithYear_Should_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "2019-04-25";
            bool areFormattedProperly = true;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }

        [TestMethod]
        public void CheckNonDateFormat_ShouldNot_BeProperlyFormatted()
        {
            //arrange
            SUPTransactionsController transaction = getTestObject();
            String dateFormatToCheck = "Hello World!";
            bool areFormattedProperly = false;

            //act
            bool actualResult = transaction.checkDateFormat(dateFormatToCheck, "04/29/19");

            //assert
            Assert.AreEqual(areFormattedProperly, actualResult);
        }
    }
}

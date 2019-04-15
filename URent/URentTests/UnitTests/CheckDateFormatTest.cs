using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using URent.Controllers;
using System.Web.Mvc;
using NUnit;

namespace URentTests.UnitTests
{
    [TestClass]
    public class CheckDateFormatTest
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
            Assert.AreEqual(actualResult, areFormattedProperly);
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
            Assert.AreEqual(actualResult, areFormattedProperly);
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
            Assert.AreEqual(actualResult, areFormattedProperly);
        }
    }
}

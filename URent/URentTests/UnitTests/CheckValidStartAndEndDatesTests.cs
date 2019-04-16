using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using URent.Controllers;
using System.Web.Mvc;
using NUnit;

namespace URentTests.UnitTests
{
    [TestClass]
    public class CheckValidStartAndEndDatesTests
    {
        private SUPTransactionsController getTestObject()
        {
            return new SUPTransactionsController();
        }

        [TestMethod]
        public void CheckIfProperDatesAreValid()
        {
            // Date format mm/dd/yyyy
            SUPTransactionsController transaction = getTestObject();
            var start = DateTime.Parse("06/05/2019");
            var end = DateTime.Parse("12/05/2019");

            var result = transaction.IsValidDate(start, end);

            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void CheckIfImproperDatesAreInvalid()
        {
            // Date format mm/dd/yyyy
            SUPTransactionsController transaction = getTestObject();
            var start = DateTime.Parse("06/05/2019");
            // End date is earlier than start date.
            var end = DateTime.Parse("05/05/2019");

            var result = transaction.IsValidDate(start, end);

            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void CheckIfSameDatesAreInvalid()
        {
            // Date format mm/dd/yyyy
            SUPTransactionsController transaction = getTestObject();
            var start = DateTime.Parse("06/05/2019");
            // End date is earlier than start date.
            var end = DateTime.Parse("06/05/2019");

            var result = transaction.IsValidDate(start, end);

            Assert.AreEqual(result, false);
        }
    }
}

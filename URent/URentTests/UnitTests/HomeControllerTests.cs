using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using URent.Abstract;
using URent.Controllers;
using URent.Models;

namespace URentTests.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {

        [TestMethod]
        public void GetListOfItemsTrue()
        {
            Mock<ISUPRepository> newMock = new Mock<ISUPRepository>();
            newMock.Setup(m => m.SUPItems).Returns(new SUPItem[]
            {
                new SUPItem{Id = 1, ItemName = "Ipad", Description = "New ipad pro.", DailyPrice = 10},
                new SUPItem{Id = 1, ItemName = "Lawn Mower", Description = "Riding lawn mower.", DailyPrice = 60},
                new SUPItem{Id = 1, ItemName = "Pot", Description = "Large cooking pot.", DailyPrice = 14},
            });

            HomeController controller = new HomeController(newMock.Object);

            List<SUPItem> items = controller.GetListOfItems();

            Assert.AreEqual(items.Count, 3);
            //Assert.AreEqual(items.First().ItemName, "Ipad");
            //Assert.AreEqual(items.Skip(1).First().ItemName, "Lawn Mower");
        }
    }
}

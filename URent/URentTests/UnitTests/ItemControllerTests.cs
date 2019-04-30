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
    public class ItemControllerTests
    {
        [TestMethod]
        public void Get_List_Of_Item_By_Query_True()
        {
            Mock<ISUPRepository> newMock = new Mock<ISUPRepository>();
            newMock.Setup(m => m.SUPItems).Returns(new SUPItem[]
            {
                new SUPItem{Id = 1, ItemName = "Ipad", Description = "New ipad pro.", DailyPrice = 10},
                new SUPItem{Id = 1, ItemName = "Old Ipad", Description = "Old ipad 2.", DailyPrice = 5},
                new SUPItem{Id = 1, ItemName = "Lawn Mower", Description = "Riding lawn mower.", DailyPrice = 60},
                new SUPItem{Id = 1, ItemName = "Pot", Description = "Large cooking pot.", DailyPrice = 14}
            });

            SUPItemsController controller = new SUPItemsController(newMock.Object);

            List<SUPItem> items = controller.GetListOfItemsWithQueryString("Ipad");

            Assert.AreEqual(items.Count, 2);
            Assert.AreEqual(items.First().Description, "New ipad pro.");
        }

        [TestMethod]
        public void Get_Item_By_User_Id_True()
        {
            Mock<ISUPRepository> newMock = new Mock<ISUPRepository>();
            newMock.Setup(m => m.SUPItems).Returns(new SUPItem[]
            {
                new SUPItem{Id = 1, ItemName = "Ipad", Description = "New ipad pro.", DailyPrice = 10, OwnerID = 2},
                new SUPItem{Id = 1, ItemName = "Old Ipad", Description = "Old ipad 2.", DailyPrice = 5, OwnerID = 1},
                new SUPItem{Id = 1, ItemName = "Lawn Mower", Description = "Riding lawn mower.", DailyPrice = 60, OwnerID = 2},
                new SUPItem{Id = 1, ItemName = "Pot", Description = "Large cooking pot.", DailyPrice = 14, OwnerID = 3},
                new SUPItem{Id = 1, ItemName = "Table", Description = "Large Table.", DailyPrice = 20, OwnerID = 2}
            });

            SUPItemsController controller = new SUPItemsController(newMock.Object);

            List<SUPItem> items = controller.GetItemsByUserId(2);

            Assert.AreEqual(items.Count, 3);
            Assert.AreEqual(items.Skip(1).First().ItemName, "Lawn Mower");
            Assert.AreEqual(items.Skip(1).First().DailyPrice, 60);
        }
    }
}

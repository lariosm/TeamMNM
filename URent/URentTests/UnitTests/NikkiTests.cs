using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using URent.Abstract;
using URent.Models;
using URent.Controllers;

namespace URentTests.UnitTests
{
    [TestClass]
    public class NikkiTests
    {
        [TestMethod]
        public void ProfileHelper_ShouldReturnProfileViewModelWithPopulatedFields_True()
        {
            //arrange
            //create mock and controller to be used
            Mock<ISUPRepository> mock = new Mock<ISUPRepository>();
            ProfileViewModel profile = new ProfileViewModel();
            ProfileViewModel profileResult = new ProfileViewModel();
            mock.Setup(m => m.SUPUsers).Returns(new SUPUser[]
            {
                new SUPUser { Id = 4, DateOfBirth = new DateTime(1967,11,04), FirstName = "Bill", LastName = "Nye", StreetAddress = "123 Sandwhich Ln.", CityAddress
                = "Monmouth", StateAddress = "Oregon", ZipCode = "96821" }
            }
            );
            SUPUsersController controller = new SUPUsersController(mock.Object);

            //act
            profileResult = controller.ProfileHelper(profile, 4);

            //assert
            Assert.AreEqual(profileResult.FirstName, "Bill");
            Assert.AreEqual(profileResult.LastName, "Nye");
        }

        [TestMethod]
        public void DetailsHelper_ShouldReturnItemDetailsViewModelWithPopulatedFields_True()
        {
            //arrange
            //create mock and controller to be used
            Mock<ISUPRepository> mock = new Mock<ISUPRepository>();
            ItemDetailsViewModel model = new ItemDetailsViewModel();
            ItemDetailsViewModel modelResult = new ItemDetailsViewModel();
            mock.Setup(m => m.SUPItems).Returns(new SUPItem[]
            {
                new SUPItem { Id = 120, ItemName = "Rolls Royce", Description = "A beautiful car that has stars on the ceiling"}
            }
            );
            SUPItemsController controller = new SUPItemsController(mock.Object);

            //act
            modelResult = controller.DetailsHelper(model, 120);

            //assert
            Assert.AreEqual(modelResult.ItemBeingReviewedID, 120);
        }
    }
}

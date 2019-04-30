using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using URent.Abstract;

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
            mock.Setup(m => m.SUPUsers).Returns(new s []
                    new SUP
                )

            //act

            //assert
        }
    }
}

using MedicineTrackingSystem.Controllers;
using NUnit.Framework;
using Moq;
using MedicineTrackingSystem;

namespace MedicineTrackingSystem_uTest
{
    [TestFixture]
    public class MedicinesControllerTests
    {
        [Test]
        public void Get_WhenCalled_ShouldInvokeAllMedicinesOfRepository( )
        {
            //Arrange
            var repositoryServiceMock = new Mock<IRepository>();
            var sut = new MedicinesController(repositoryServiceMock.Object);

            //Act
            sut.Get();

            //Assert
            repositoryServiceMock.Verify(m => m.AllMedicines, Times.Once);
        }

        [Test]
        public void SearchMedicineByName_WhenCalled_ShouldInvokeGetMedicineOfRepository()
        {
            //Arrange
            var repositoryServiceMock = new Mock<IRepository>();
            var sut = new MedicinesController(repositoryServiceMock.Object);

            //Act
            sut.SearchMedicineByName("name");

            //Assert
            repositoryServiceMock.Verify(m => m.GetMedicine(It.IsAny <string>()), Times.Once);
        }

        [Test]
        public void CreateNewEntry_WhenCalled_ShouldInvokeCreateNewEntryOfRepository()
        {
            //Arrange
            var repositoryServiceMock = new Mock<IRepository>();
            var sut = new MedicinesController(repositoryServiceMock.Object);

            //Act
            sut.CreateNewEntry(new Medicine());

            //Assert
            repositoryServiceMock.Verify(m => m.CreateNewEntry(It.IsAny<Medicine>()), Times.Once);
        }
    }
}
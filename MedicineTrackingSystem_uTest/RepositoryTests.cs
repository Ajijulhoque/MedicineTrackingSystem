using System;
using NUnit.Framework;
using Moq;
using MedicineTrackingSystem;
using System.Collections.Generic;

namespace MedicineTrackingSystem_uTest
{
    [TestFixture]
    public class RepositoryTests
    {
        [Test]
        public void AllMedicines_WhenCalled_ShouldReturnAllMedicineDetails()
        {
            //Arrange
            var jsonReaderMock = new Mock<IJsonReader>();
            var jsonWriterMock = new Mock<IJsonWriter>();
            var medicineRepository = GetMedicineRepository();

            jsonReaderMock.Setup(r => r.ReadJsonFile(It.IsAny<string>())).Returns(medicineRepository);

            var sut = new Repository(jsonReaderMock.Object, jsonWriterMock.Object);

            //Act
            var actualMedicines = sut.AllMedicines;

            //Assert
            Assert.AreEqual(medicineRepository.Medicines.Count, actualMedicines.Count);
        }

        [Test]
        public void GetMedicine_WhenCalled_ShouldReturnMedicineDetails()
        {
            //Arrange
            var jsonReaderMock = new Mock<IJsonReader>();
            var jsonWriterMock = new Mock<IJsonWriter>();
            var medicineRepository = GetMedicineRepository();
            jsonReaderMock.Setup(r => r.ReadJsonFile(It.IsAny<string>())).Returns(medicineRepository);

            var sut = new Repository(jsonReaderMock.Object, jsonWriterMock.Object);

            //Act
            var actualMedicine = sut.GetMedicine("Med1");

            //Assert
            Assert.AreEqual("Med1",actualMedicine.FullName);
            Assert.AreEqual("Brand1", actualMedicine.Brand);
            Assert.AreEqual(300, actualMedicine.Price);
            Assert.AreEqual(10, actualMedicine.Quantity);
            Assert.AreEqual("Its a note1", actualMedicine.Notes);
        }

        [Test]
        public void CreateNewEntrys_WhenCalled_ShouldCreateNewEntry()
        {
            //Arrange
            var jsonReaderMock = new Mock<IJsonReader>();
            var jsonWriterMock = new Mock<IJsonWriter>();
            var medicine = new Medicine
            {
                FullName = "Med3",
                Brand = "Brand3",
                Price = 500,
                Quantity = 15,
                Notes = "Its a note3"
            };

            var medicineRepository = GetMedicineRepository();

            jsonReaderMock.Setup(r => r.ReadJsonFile(It.IsAny<string>())).Returns(medicineRepository);

            var sut = new Repository(jsonReaderMock.Object, jsonWriterMock.Object);

            //Act
            sut.CreateNewEntry(medicine);

            //Assert
            Assert.AreEqual(3, medicineRepository.Medicines.Count);
            jsonWriterMock.Verify(wr => wr.WriteToJsonFile(It.IsAny<string>(), medicineRepository), Times.Once);
        }

        private static MedicineRepository GetMedicineRepository()
        {
            var medicines = new List<Medicine>
            {
                new Medicine
                {
                    FullName = "Med1",
                    Brand  = "Brand1",
                    Price = 300,
                    Quantity = 10,
                    Notes = "Its a note1"
                },
                new Medicine
                {
                    FullName = "Med2",
                    Brand  = "Brand2",
                    Price = 400,
                    Quantity = 20,
                    Notes = "Its a note2"
                }
            };

            return new MedicineRepository
            {
                Medicines = medicines
            };
        }
    }
}

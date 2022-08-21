using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            [Test]
            public void GarageNameShouldReturnExceptionWithEmptyOrNullValues()
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    var garage = new Garage(null, 10);
                },

                "Invalid garage name");

                Assert.Throws<ArgumentNullException>(() =>
                {
                    var garage = new Garage(string.Empty, 10);
                },

                "Invalid garage name");

            }
            [Test]
            public void GaragePropertyShouldWorkCorrectly()
            {
                const string name = "Test";
                var garage = new Garage(name, 10);
                Assert.That(garage.Name, Is.EqualTo(name));
            }
            [Test]
            public void MachanicAvailableMustNotBeZeroOrNevative()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    var garage = new Garage("Pesho", 0);
                },

                "At least one mechanic must work in the garage.");

                Assert.Throws<ArgumentException>(() =>
                {
                    var garage = new Garage("Pesho", -1);
                },

                "At least one mechanic must work in the garage.");

            }
            [Test]
            public void MachanicsAvailableProperyShouldWorkProperly()
            {
                const int mechanicsAvailable = 10;
                var garage = new Garage("Pesho", 10);
                Assert.That(garage.MechanicsAvailable, Is.EqualTo(mechanicsAvailable));
            }
            [Test]
            public void AddCarShouldNotWorkIfNoMecsAvailable()
            {
                //arrange
                var garage = new Garage("Test", 1);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car("Honda", 2);
                //act
                garage.AddCar(firstCar);
                //assert
                Assert.Throws<InvalidOperationException>(() =>
                { garage.AddCar(secondCar); },
                "No mechanic available.");

            }
            [Test]
            public void AddCarCountShouldWorkProperly()
            {
                //arrange
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car("Honda", 2);
                //act
                garage.AddCar(firstCar);
                garage.AddCar(secondCar);
                //assert
                Assert.That(garage.CarsInGarage, Is.EqualTo(2));

            }
            [Test]
            public void GarageShouldThrowExceptionIfCarIsMissing()
            {
                //arrange
                const string carModel = "Honda";
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car(carModel, 2);
                //act
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar(carModel);
                }
                //assert
                , $"The car {carModel} doesn't exist.");
            }
            [Test]
            public void GarageFixedCarShouldBeReturnedIfExist()
            {
                //arrage
                const string carModel = "Honda";
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car(carModel, 2);
                //act
                garage.AddCar(secondCar);
                var fixedCar = garage.FixCar(carModel);
                //assert
                Assert.That(fixedCar.IsFixed, Is.True);
                Assert.That(fixedCar.CarModel, Is.EqualTo(carModel));
                Assert.That(fixedCar.NumberOfIssues, Is.EqualTo(0));
            }
            [Test]
            public void GarageRemoveFixCarIsFixedCarsAreAvailable()
            {
                //arrange
                const string carModel = "Honda";
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car(carModel, 2);
                //act
                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();
                }
                //assert
                , $"The car {carModel} doesn't exist.");
            }
            public void GarageRemoveFixCarShouldRemoveFixedCar()
            {
                //arrange
                const string carModel = "Honda";
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car(carModel, 2);
                var thirdCar = new Car("BMW", 3);
                //act
                garage.AddCar(firstCar);
                garage.AddCar(secondCar);
                garage.AddCar(thirdCar);
                var fixedCar = garage.RemoveFixedCar();
                //Assert
                Assert.That(garage.RemoveFixedCar, Is.EqualTo(1));
                Assert.That(garage.CarsInGarage, Is.EqualTo(2));
            }
            [Test]
            public void GarageReportShouldShowCorrectResult()
            {
                //arrange
                const string carModel = "Honda";
                var garage = new Garage("Test", 3);
                var firstCar = new Car("Toyota", 2);
                var secondCar = new Car(carModel, 2);
                var thirdCar = new Car("BMW", 3);
                //act
                garage.AddCar(firstCar);
                garage.AddCar(secondCar);
                garage.AddCar(thirdCar);
                garage.FixCar(carModel);
                var carReport = garage.Report();
                //assert
                Assert.That(carReport, Is.EqualTo($"There are 2 which are not fixed: Toyota, BMW."));
            }
        }

    }
}
using System;
using System.Threading.Tasks;
using CongestionTaxCalculator.Common;
using CongestionTaxCalculator.Service;
using CongestionTaxCalculator.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CongestionTaxCalculator.Test
{
    [TestClass]
    public class CalculateTaxServicesTests
    {
        private readonly ICalculateTaxService _calculateTaxService;

        public CalculateTaxServicesTests()
        {
            _calculateTaxService = new CalculateTaxService(new TaxRulesLocal());
        }

        [TestMethod]
        public async Task ReturnCorrectFeeBeforeSix()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 5, 59, 0)
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public async Task ReturnCorrectFeeAtSix()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 6, 0, 0)
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(8, result);
        }


        [TestMethod]
        public async Task ReturnCorrectFeeForThreeDates()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 6, 0, 0),
                new DateTime(2013, 02, 08, 6, 15, 0),
                new DateTime(2013, 02, 08, 9, 0, 0)
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(16, result);
        }

        [TestMethod]
        public async Task ReturnCorrectFeeForSixDates()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 6, 0, 0),
                new DateTime(2013, 02, 08, 6, 15, 0),
                new DateTime(2013, 02, 08, 9, 30, 0),
                new DateTime(2013, 02, 08, 12, 30, 0),
                new DateTime(2013, 02, 08, 12, 40, 0),
                new DateTime(2013, 02, 08, 17, 30, 0)
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(37, result);
        }

        [TestMethod]
        public async Task ReturnCorrectFeeForSixDatesOutOfOrder()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 17, 30, 0),
                new DateTime(2013, 02, 08, 6, 0, 0),
                new DateTime(2013, 02, 08, 12, 40, 0),
                new DateTime(2013, 02, 08, 9, 30, 0),
                new DateTime(2013, 02, 08, 12, 30, 0),
                new DateTime(2013, 02, 08, 6, 15, 0),
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(37, result);
        }

        [TestMethod]
        public async Task ReturnNoFeeForExemptVehicleType()
        {
            var vehicleType = VehicleType.Bus;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 6, 0, 0),
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public async Task ReturnMaxFeePerDay()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 6, 30, 0),
                new DateTime(2013, 02, 08, 7, 0, 0),
                new DateTime(2013, 02, 08, 8, 0, 0),
                new DateTime(2013, 02, 08, 9, 30, 0),
                new DateTime(2013, 02, 08, 10, 30, 0),
                new DateTime(2013, 02, 08, 11, 30, 0),
                new DateTime(2013, 02, 08, 12, 30, 0),
                new DateTime(2013, 02, 08, 13, 30, 0),
                new DateTime(2013, 02, 08, 16, 30, 0),
                new DateTime(2013, 02, 08, 18, 30, 0),
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(60, result);
        }

        [TestMethod]
        public async Task ReturnHighestFeeForTwoWithinOneHour()
        {
            var vehicleType = VehicleType.Car;
            var passageDateTimes = new[]
            {
                new DateTime(2013, 02, 08, 15, 00, 0),
                new DateTime(2013, 02, 08, 15, 30, 0),
            };

            var result = await _calculateTaxService.CalculateTax(vehicleType, passageDateTimes);

            Assert.AreEqual(18, result);
        }
    }
}

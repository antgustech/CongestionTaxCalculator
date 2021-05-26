using System;
using System.Threading.Tasks;
using CongestionTaxCalculator.Api.Controllers;
using CongestionTaxCalculator.Api.Models;
using CongestionTaxCalculator.Common;
using CongestionTaxCalculator.Service;
using CongestionTaxCalculator.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CongestionTaxCalculator.Test
{
    /// <summary>
    /// As most logic is decoupled from Controllers, its easier and better to just test the services separately instead.
    /// However I added this to give a general idea on how the API works.
    /// </summary>
    [TestClass]
    public class ApiTests
    {
        [TestMethod]
        public async Task TestPost()
        {
            CongestionTaxCalculatorRequest congestionTaxCalculatorRequest = new CongestionTaxCalculatorRequest
            {
                VehicleType = VehicleType.Car,
                PassageDateTimes = new[]
                {
                    new DateTime(2013, 02, 08, 6, 0, 0)
                }
            };

            var controller = new CongestionTaxCalculatorController(new CalculateTaxService(new TaxRulesLocal()));
            var actionResult = await controller.Post(congestionTaxCalculatorRequest);

            var okResult = actionResult as OkObjectResult;
            Assert.AreEqual(200, okResult.StatusCode);
            var value = okResult.Value as CongestionTaxCalculatorResponse;
            Assert.AreEqual(value.Fee, 8);
        }
    }
}

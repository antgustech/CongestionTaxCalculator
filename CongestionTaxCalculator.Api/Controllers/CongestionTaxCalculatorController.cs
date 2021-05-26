using System;
using System.Net;
using CongestionTaxCalculator.Api.Models;
using CongestionTaxCalculator.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Api.Controllers
{
    [ApiController]
    [Route("calculate/tax")]
    [ApiVersion("1.0")]
    public class CongestionTaxCalculatorController : ControllerBase
    {
        private readonly ICalculateTaxService _calculateTaxService;

        public CongestionTaxCalculatorController(ICalculateTaxService calculateTaxService)
        {
            _calculateTaxService = calculateTaxService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CongestionTaxCalculatorRequest request)
        {
            try
            {
                var totalFee = await _calculateTaxService.CalculateTax(request.VehicleType.Value, request.PassageDateTimes);

                return Ok(new CongestionTaxCalculatorResponse {Fee = totalFee});
            }
            catch (Exception e) //TODO Could be improved with more specific errors.
            {
                return StatusCode((int) HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using CongestionTaxCalculator.Common;

namespace CongestionTaxCalculator.Api.Models
{
    public class CongestionTaxCalculatorRequest
    {
        [Required] public VehicleType? VehicleType { get; set; }

        [Required] public DateTime[] PassageDateTimes { get; set; }
    }
}
using System;
using System.Threading.Tasks;
using CongestionTaxCalculator.Common;

namespace CongestionTaxCalculator.Service
{
    public interface ICalculateTaxService
    { 
        Task<decimal> CalculateTax(VehicleType vehicleType, DateTime[] passageDateTimes);
    }
}
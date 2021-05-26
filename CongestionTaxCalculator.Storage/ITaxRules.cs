using System.Collections.Generic;
using System.Threading.Tasks;
using CongestionTaxCalculator.Common;


namespace CongestionTaxCalculator.Storage
{
    public interface ITaxRules
    {
        Task<IEnumerable<TaxInterval>> GetTaxIntervalsAsync();
        Task<IEnumerable<VehicleType>> GetExemptVehicleTypesAsync();
    }
}

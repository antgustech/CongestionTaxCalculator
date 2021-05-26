using System.Collections.Generic;
using System.Threading.Tasks;
using CongestionTaxCalculator.Common;

namespace CongestionTaxCalculator.Storage
{
    public class TaxRulesLocal : ITaxRules
    {
        //TODO this code is actually not really async as we have hardcoded values but for other handlers it would be preferred
        public async Task<IEnumerable<TaxInterval>> GetTaxIntervalsAsync()
        {
            return new List<TaxInterval>()
                {
                    new TaxInterval(06, 06, 00, 29, 8),
                    new TaxInterval(06, 06, 30, 59, 13),
                    new TaxInterval(07, 07, 00, 59, 18),
                    new TaxInterval(08, 08, 00, 29, 13),
                    
                    new TaxInterval(08, 14, 30, 59, 8),
                    new TaxInterval(09, 14, 00, 59, 8),

                    new TaxInterval(15, 15, 00, 29, 13),
                    new TaxInterval(15, 16, 30, 59, 18),
                    new TaxInterval(17, 17, 00, 59, 13),
                    new TaxInterval(18, 18, 00, 29, 8),

                    new TaxInterval(18, 05, 30, 59, 0),
                    new TaxInterval(19, 05, 00, 59, 0)
                };
        }

        public async Task<IEnumerable<VehicleType>> GetExemptVehicleTypesAsync()
        {
            return new List<VehicleType>()
            {
                VehicleType.Emergency,
                VehicleType.Bus,
                VehicleType.Diplomat,
                VehicleType.Motorcycle,
                VehicleType.Military,
                VehicleType.Foreign
            };
        }
    }
}

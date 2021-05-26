using System;
using System.Linq;
using System.Threading.Tasks;
using CongestionTaxCalculator.Common;
using CongestionTaxCalculator.Storage;
using Nager.Date;

namespace CongestionTaxCalculator.Service
{
    public class CalculateTaxService : ICalculateTaxService
    {
        private const int MaxDailyFee = 60;
        private readonly ITaxRules _taxRules;

        public CalculateTaxService(ITaxRules taxRules)
        {
            _taxRules = taxRules;
        }

        public async Task<decimal> CalculateTax(VehicleType vehicleType, DateTime[] passageDateTimes)

        {
            passageDateTimes = passageDateTimes.OrderBy(d => d).ToArray();
            DateTime passageDateTimeStart = passageDateTimes[0];
            decimal totalFee = 0;
            foreach (DateTime passageDateTime in passageDateTimes)
            {
                decimal nextFee = await GetTaxFee(passageDateTime, vehicleType);
                decimal currentFee = await GetTaxFee(passageDateTimeStart, vehicleType);

                //A single charge rule applies in Gothenburg.
                //Under this rule, a vehicle that passes several tolling stations within 60 minutes is only taxed once.
                //The amount that must be paid is the highest one.
                if ((passageDateTime - passageDateTimeStart) <= TimeSpan.FromMinutes(60))
                {
                    if (totalFee > 0)
                    {
                        totalFee -= currentFee;
                    }

                    if (nextFee >= currentFee)
                    {
                        currentFee = nextFee;
                    }

                    totalFee += currentFee;
                }
                else
                {
                    totalFee += nextFee;
                    passageDateTimeStart = passageDateTime;
                }
            }

            if (totalFee > MaxDailyFee)
            {
                totalFee = MaxDailyFee;
            }

            return totalFee;
        }

        private async Task<bool> IsTaxFreeVehicleType(VehicleType vehicleType)
        {
            var vehicleTypes = await _taxRules.GetExemptVehicleTypesAsync();

            return vehicleTypes.Contains(vehicleType);
        }

        private bool IsTaxFreePassageDate(DateTime passageDate)
        {
            if (passageDate.Year != 2013) return false; //Assume that this is only for 2013.

            return passageDate.DayOfWeek == DayOfWeek.Saturday ||
                   passageDate.DayOfWeek == DayOfWeek.Sunday ||
                   DateSystem.IsPublicHoliday(passageDate, CountryCode.SE);
        }

        public async Task<decimal> GetTaxFee(DateTime passageDate, VehicleType vehicleType)
        {
            if (IsTaxFreePassageDate(passageDate) || await IsTaxFreeVehicleType(vehicleType)) return 0;

            var taxIntervals = await _taxRules.GetTaxIntervalsAsync();

            foreach (var timeInterval in taxIntervals)
            {
                var fee = timeInterval.Fee(passageDate);
                if (fee > 0)
                {
                    return fee;
                }
            }

            return 0;
        }
    }
}

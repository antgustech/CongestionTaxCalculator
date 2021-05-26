using System;

namespace CongestionTaxCalculator.Common
{
    public class TaxInterval
    {
        private readonly decimal _fee;

        private readonly int _hourStart;
        private readonly int _hourEnd;

        private readonly int _minuteStart;
        private readonly int _minuteEnd;

        public TaxInterval(int hourStart, int hourEnd, int minuteStart, int minuteEnd, decimal fee)
        {
            _hourStart = hourStart;
            _hourEnd = hourEnd;
            _minuteStart = minuteStart;
            _minuteEnd = minuteEnd;
            _fee = fee;
        }

        public decimal Fee(DateTime moment)
        {
            int hour = moment.Hour;
            int minute = moment.Minute;

            if ((hour >= _hourStart && hour <= _hourEnd) && (minute >= _minuteStart && minute <= _minuteEnd))
            {
                return _fee;
            }

            return 0;
        }
    }
}

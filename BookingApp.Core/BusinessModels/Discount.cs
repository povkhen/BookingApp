using System;
using System.Globalization;

namespace BookingApp.Core.BusinessModels
{
    class Discount
    {
        private string _specifier = "C";
        private CultureInfo _culture = CultureInfo.CreateSpecificCulture("uk-UA");
        public Discount(double val, int day)
        {
            Value = val;
            Day = day;
        }
        public double Value { get; } = 0;
        public int Day { get; } = 0;
        public string GetDiscountedPrice(double sum) =>
            SelectPrice(Day, sum, Value).ToString(_specifier, _culture);
        
        static double SelectPrice(int day, double sum, double persent)
        {
            switch (day)
            {
                case 1: return sum + sum * persent;
                case 2: return sum - sum * persent;
                case 3: return sum - sum * persent*0.9;
                case 4: return sum;
                case 5: return sum;
                case 6: return sum;
                case 7: return sum + sum * persent;

                default: return sum;
            }
        }
    }
}

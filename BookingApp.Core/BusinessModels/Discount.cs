using System;

namespace BookingApp.Core.BusinessModels
{
    class Discount
    {
        public Discount(double val)
        {
            Value = val;
        }
        public double Value { get; } = 0;
        public double GetDiscountedPrice(double sum)
        {
            if (DateTime.Now.Day == 1)
                return sum - sum * Value;
            return sum;
        }
    }
}

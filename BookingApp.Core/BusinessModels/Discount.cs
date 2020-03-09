using System;

namespace BookingApp.Core.BusinessModels
{
    class Discount
    {
        private decimal _value = 0;
        public Discount(decimal val)
        {
            _value = val;
        }
        public decimal Value { get { return _value; } }
        public decimal GetDiscountedPrice(decimal sum)
        {
            if (DateTime.Now.Day == 1)
                return sum - sum * _value;
            return sum;
        }
    }
}

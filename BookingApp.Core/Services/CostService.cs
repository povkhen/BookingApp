using BookingApp.Core.BusinessModels;
using BookingApp.Core.Interfaces;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BookingApp.Core.Services
{
    public class CostService : ICostService
    {
        private string _specifier = "C";
        private CultureInfo _culture = CultureInfo.CreateSpecificCulture("uk-UA");

        public async Task<string> GetCostOfSeatAsync(double priceCoeff, string durationStr, int day)
        {
            double duration = TimeSpan.Parse(durationStr).TotalMinutes;
            string res = (duration/priceCoeff).ToString(_specifier, _culture);
            return await Task.FromResult(res);
        }

        public async Task<string> GetSaleCostOfSeatAsync(double priceCoeff, string durationStr, int day)
        {

            double duration = TimeSpan.Parse(durationStr).TotalMinutes;
            string res = new Discount(0.5, day).GetDiscountedPrice(duration / priceCoeff);
            return await Task.FromResult(res);
        }
    }
}

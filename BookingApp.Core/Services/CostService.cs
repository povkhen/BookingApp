using BookingApp.Core.BusinessModels;
using BookingApp.Core.Interfaces;
using System.Threading.Tasks;

namespace BookingApp.Core.Services
{
    public class CostService : ICostService
    {
        
        public async Task<double> GetCostOfSeatAsync(double priceCoeff, double duration)
        {
            double res = new Discount(0.5).GetDiscountedPrice(duration/priceCoeff);
            return await Task.FromResult(res);
        }

    }
}

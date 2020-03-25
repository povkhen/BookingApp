using System.Threading.Tasks;

namespace BookingApp.Core.Interfaces
{
    public interface ICostService
    {
        Task<double> GetCostOfSeatAsync(double priceCoeff, double duration);
    }
}

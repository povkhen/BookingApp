using System.Threading.Tasks;

namespace BookingApp.Core.Interfaces
{
    public interface ICostService
    {
        Task<string> GetCostOfSeatAsync(double priceCoeff, string duration, int day);
        Task<string> GetSaleCostOfSeatAsync(double priceCoeff, string duration, int day);
    }
}

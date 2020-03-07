using BookingApp.Data.Entities;
using System.Threading.Tasks;

namespace BookingApp.Data.Interfaces
{
    public interface IStationRepository : IRepository<Station>
    {
        Task<Station> FindStationByNameAsync(string name);
    }
}

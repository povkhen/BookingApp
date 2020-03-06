using BookingApp.Core.Entities;
using System.Threading.Tasks;

namespace BookingApp.Core.DataService
{
    public interface IStationRepository : IRepository<Station>
    {
        Task<Station> FindStationByNameAsync(string name);
    }
}

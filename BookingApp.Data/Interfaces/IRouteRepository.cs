using BookingApp.Data.Entities;
using System.Threading.Tasks;

namespace BookingApp.Data.Interfaces
{
    public interface IRouteRepository : IRepository<Route>
    {
        Task<Route> FindRouteByNameAsync(string name);
    }
}

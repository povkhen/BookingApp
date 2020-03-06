using BookingApp.Core.Entities;
using System.Threading.Tasks;

namespace BookingApp.Core.DataService
{
    public interface IRouteRepository : IRepository<Route>
    {
        Task<Route> FindRouteByNameAsync(string name);
    }
}

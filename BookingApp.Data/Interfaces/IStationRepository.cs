using BookingApp.Data.Entities;
using System;
using System.Threading.Tasks;

namespace BookingApp.Data.Interfaces
{
    public interface IStationRepository : IRepository<Station>
    {
        Task<Guid?> FindStationIdByNameAsync(string name);
        Task<Station> FindStationByNameAsync(string name);
    }
}

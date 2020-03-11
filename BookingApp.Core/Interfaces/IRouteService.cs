using BookingApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Core.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<TripSearchDTO>> SearchTrip(string departureStatiom, string arrivalStation, DateTime date);
        Task<IEnumerable<TypeCarSeatsDTO>> SearchFreeSeatById(Guid id, string from, string to);
        Task<IEnumerable<string>> GetAllTypesCarName();
        Task<IEnumerable<string>> GetAllStationsName();
        Task<bool> ExistStationByName(string name);
        Task<bool> ExistRouteByName(string name);
        Task<dynamic> GetRouteInfo(string route);
    }
}

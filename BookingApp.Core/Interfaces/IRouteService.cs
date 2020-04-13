using BookingApp.Core.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Core.Interfaces
{
    public interface IRouteService
    {
        Task<IEnumerable<TripSearchDTO>> SearchTrip(string departureStatiom, string arrivalStation, DateTime date);
        Task<IEnumerable<AllrSeatsProcedureDTO>> SearchAllSeatById(Guid id, string from, string to, string typecar);
        Task<IEnumerable<TypeCarSeatsDTO>> SearchFreeSeatById(Guid id, string from, string to);
        Task<IEnumerable<string>> GetAllTypesCarName();
        Task<IEnumerable<StationDTO>> GetAllStations();
        Task<Guid?> GetIdStation(string name);
        Task<bool> ExistStationByName(string name);
        Task<bool> ExistRouteByName(string name);
        Task<dynamic> GetRouteInfo(string route);
    }
}

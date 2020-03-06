using BookingApp.Core.Entities;
using BookingApp.Core.Entities.Procedure_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Core.ApplicationService
{
    public interface IRouteService
    {
        Task<IEnumerable<TripSearch>> SearchTrip(string departureStatiom, string arrivalStation, DateTime date);
        Task<IEnumerable<TypeCarSeats>> SearchFreeSeatById(Guid id, string from, string to);
        Task<bool> ExistStationByName(string name);
        Task<bool> ExistRouteByName(string name);
        Task<IEnumerable<string>> GetAllTypesCarName();
        Task<dynamic> GetRouteInfo(string route);
    }
}

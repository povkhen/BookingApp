using BookingApp.Core.Entities.Procedure_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Core.DataService
{
    public interface IStoredProcedures
    {
        Task<IEnumerable<TripSearch>> GetSearchTrips(string departureStation, string arrivalStation, DateTime date);
        Task<IEnumerable<TypeCarSeats>> GetFreeGroupingSeats(Guid tripId, string from, string to);
        Task<IEnumerable<dynamic>> GetRouteInfo(string route);
    }
}

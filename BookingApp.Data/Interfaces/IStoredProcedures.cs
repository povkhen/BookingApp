using BookingApp.Data.Entities.Procedure_Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Data.Interfaces
{
    public interface IStoredProcedures
    {
        Task<IEnumerable<TripSearch>> GetSearchTrips(string departureStation, string arrivalStation, DateTime date);
        Task<IEnumerable<TypeCarSeats>> GetFreeGroupingSeats(Guid tripId, string from, string to);
        Task<IEnumerable<AllrSeatsProcedure>> GetAllSeats(Guid tripId, string from, string to, string typecar);
        Task<IEnumerable<dynamic>> GetRouteInfo(string route);
    }
}

using BookingApp.Core.ApplicationService;
using BookingApp.Core.DataService;
using BookingApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BookingApp.Core.Entities.Procedure_Models;
using System.Linq;

namespace BookingApp.Core.Services
{
    public class RouteService : IRouteService
    {
        protected readonly IContext _context;
        public RouteService() : this (new Context()) { }
       
        public RouteService(IContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistStationByName(string name)
        {
            try
            {
                await _context.StationRepo.FindStationByNameAsync(name);                
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            return true;
             
        }

        public async Task<bool> ExistRouteByName(string name)
        {
            try
            {
                await _context.RouteRepo.FindRouteByNameAsync(name);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            return true;

        }

        public async Task<IEnumerable<string>> GetAllTypesCarName()
        {
            var typeCars = await _context.TypeCarRepo.GetAllAsync();
            return typeCars.ToList().Select(x => x.Name);
        }

        public async Task<dynamic> GetRouteInfo(string route)
        {
            return await _context.StoredProcedures.GetRouteInfo(route);
        }

        public async Task<IEnumerable<TypeCarSeats>> SearchFreeSeatById(Guid id, string from, string to)
        {
            return await _context.StoredProcedures.GetFreeGroupingSeats(id, from, to);
        }

        public async Task<IEnumerable<TripSearch>> SearchTrip(string departureStatiom, string arrivalStation, DateTime date)
        {
            return await _context.StoredProcedures.GetSearchTrips(departureStatiom, arrivalStation, date);           
        }
    }
}

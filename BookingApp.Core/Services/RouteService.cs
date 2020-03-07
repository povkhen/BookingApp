using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using BookingApp.Data.Interfaces;
using BookingApp.Data.Entities.Procedure_Models;
using BookingApp.Core.Interfaces;
using BookingApp.Core.DTO;
using AutoMapper;

namespace BookingApp.Core.Services
{
    public class RouteService : IRouteService
    {
        protected readonly IUnitOfWork _unitOfWork;
       
        public RouteService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExistStationByName(string name)
        {
            try
            {
                await _unitOfWork.StationRepo.FindStationByNameAsync(name);                
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
                await _unitOfWork.RouteRepo.FindRouteByNameAsync(name);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
            return true;

        }

        public async Task<IEnumerable<string>> GetAllTypesCarName()
        {
            var typeCars = await _unitOfWork.TypeCarRepo.GetAllAsync();
            return typeCars.ToList().Select(x => x.Name);
        }

        public async Task<dynamic> GetRouteInfo(string route)
        {
            return await _unitOfWork.StoredProcedures.GetRouteInfo(route);
        }

        public async Task<IEnumerable<TypeCarSeatsDTO>> SearchFreeSeatById(Guid id, string from, string to)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeCarSeats,TypeCarSeatsDTO>()).CreateMapper();
            var seats = await _unitOfWork.StoredProcedures.GetFreeGroupingSeats(id, from, to);
            var res = mapper.Map<IEnumerable<TypeCarSeats>, IEnumerable<TypeCarSeatsDTO>>(seats);
            return await Task.FromResult(res);
        }

        public async Task<IEnumerable<TripSearchDTO>> SearchTrip(string departureStatiom, string arrivalStation, DateTime date)
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TripSearch, TripSearchDTO>()).CreateMapper();
            var trips =  await _unitOfWork.StoredProcedures.GetSearchTrips(departureStatiom, arrivalStation, date);
            var res = mapper.Map<IEnumerable<TripSearch>, List<TripSearchDTO>>(trips);
            return await Task.FromResult(res);
        }
    }
}

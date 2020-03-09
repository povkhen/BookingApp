using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using AutoMapper;

using BookingApp.Data.Interfaces;
using BookingApp.Data.Entities.Procedure_Models;
using BookingApp.Data.Infrastructure;
using BookingApp.Core.Interfaces;
using BookingApp.Core.DTO;

namespace BookingApp.Core.Services
{
    public class RouteService : IRouteService
    {
        protected readonly IDALSession _dalSession;
        protected readonly IContext _context;

        public RouteService(IDALSession dalSession, IContext context)
        {
            _dalSession = dalSession;
            _context = context;
        }

        public async Task<bool> ExistStationByName(string name)
        {
            using (_dalSession)
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    await _context.StationRepo.FindStationByNameAsync(name);
                    unitOfWork.Commit();
                }
                catch (KeyNotFoundException)
                {
                    unitOfWork.Rollback();
                    return false;
                }
                return true;    
            }
        }

        public async Task<bool> ExistRouteByName(string name)
        {
            using (_dalSession)
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    await _context.RouteRepo.FindRouteByNameAsync(name);
                    unitOfWork.Commit();
                    
                }
                catch (KeyNotFoundException)
                {
                    unitOfWork.Rollback();
                    return false;
                }
                return true;

            }
        }

        public async Task<IEnumerable<string>> GetAllTypesCarName()
        {
            using (_dalSession)
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var typeCars = await _context.TypeCarRepo.GetAllAsync();
                    unitOfWork.Commit();
                    return typeCars.ToList().Select(x => x.Name);

                }
                catch
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }

        public async Task<dynamic> GetRouteInfo(string route)
        {
            using (_dalSession)
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var inforoute = await _context.StoredProcedures.GetRouteInfo(route);
                    unitOfWork.Commit();
                    return inforoute;

                }
                catch
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }

        public async Task<IEnumerable<TypeCarSeatsDTO>> SearchFreeSeatById(Guid id, string from, string to)
        {
            using (_dalSession)
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TypeCarSeats, TypeCarSeatsDTO>()).CreateMapper();
                    var seats = await _context.StoredProcedures.GetFreeGroupingSeats(id, from, to);
                    var res = mapper.Map<IEnumerable<TypeCarSeats>, IEnumerable<TypeCarSeatsDTO>>(seats);
                    unitOfWork.Commit();
                    return await Task.FromResult(res);                   

                }
                catch
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }

        public async Task<IEnumerable<TripSearchDTO>> SearchTrip(string departureStatiom, string arrivalStation, DateTime date)
        {
            using (_dalSession)
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TripSearch, TripSearchDTO>()).CreateMapper();
                    var trips = await _context.StoredProcedures.GetSearchTrips(departureStatiom, arrivalStation, date);
                    var res = mapper.Map<IEnumerable<TripSearch>, List<TripSearchDTO>>(trips);
                    unitOfWork.Commit();
                    return await Task.FromResult(res);

                }
                catch
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }
    }
}

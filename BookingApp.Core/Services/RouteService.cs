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
using BookingApp.Data.Entities;

namespace BookingApp.Core.Services
{
    public class RouteService : IRouteService
    {
        protected readonly IContext _context;
        protected readonly IMapper _mapper;
        public RouteService(IContext context)
        {
            _context = context;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TypeCarSeats, TypeCarSeatsDTO>();
                cfg.CreateMap<TripSearch, TripSearchDTO>();
                cfg.CreateMap<AllrSeatsProcedure, AllrSeatsProcedureDTO>();
                cfg.CreateMap<Station, StationDTO>();
            }).CreateMapper();
        }

        public async Task<bool> ExistStationByName(string name)
        {
            using (IDALSession _dalSession = new DalSession())
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
            using (IDALSession _dalSession = new DalSession())
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
            using (IDALSession _dalSession = new DalSession())
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
            using (IDALSession _dalSession = new DalSession())
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
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var seats = await _context.StoredProcedures.GetFreeGroupingSeats(id, from, to);
                    var res = _mapper.Map<IEnumerable<TypeCarSeats>, List<TypeCarSeatsDTO>>(seats);
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

        public async Task<IEnumerable<AllrSeatsProcedureDTO>> SearchAllSeatById(Guid id, string from, string to, string typecar)
        {
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var allseats = await _context.StoredProcedures.GetAllSeats(id, from, to, typecar);
                    var res = _mapper.Map<IEnumerable<AllrSeatsProcedure>, List<AllrSeatsProcedureDTO>>(allseats);
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
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var trips = await _context.StoredProcedures.GetSearchTrips(departureStatiom, arrivalStation, date);
                    var res = _mapper.Map<IEnumerable<TripSearch>, List<TripSearchDTO>>(trips);
                    foreach (var trip in res)
                    {
                        trip.FreeSeats = await this.SearchFreeSeatById(trip.Id, departureStatiom, arrivalStation);
                    }
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

        public async Task<IEnumerable<StationDTO>> GetAllStations()
        {
            using (IDALSession _dalSession = new DalSession())
            {
                UnitOfWork unitOfWork = _dalSession.UnitOfWork;
                unitOfWork.Begin();
                try
                {
                    var stations = await _context.StationRepo.GetAllAsync();
                    var res = _mapper.Map<IEnumerable<Station>, List<StationDTO>>(stations);
                    unitOfWork.Commit();
                    return await Task.FromResult(res);

                }
                catch (KeyNotFoundException)
                {
                    unitOfWork.Rollback();
                    return null;
                }
            }
        }
    }
}

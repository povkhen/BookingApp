using BookingApp.Data.Entities.Procedure_Models;
using BookingApp.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BookingApp.Data.Infrastructure
{
    public class StoredProcedures : IStoredProcedures
    {
        public async Task<IEnumerable<TripSearch>> GetSearchTrips(string departureStation, string arrivalStation, DateTime date)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                string sqlFormattedDate = date.ToString("yyyy-MM-dd");
                var procedure = "[route].[SEARCHPROC]";
                var values = new { StartStation = departureStation, EndStation = arrivalStation, Date = sqlFormattedDate };
                var result = await connection.QueryAsync<TripSearch>(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<TypeCarSeats>> GetFreeGroupingSeats(Guid tripId, string from, string to)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var procedure = "[schedule].[GET_GROUPING_SEATS]";
                var values = new {From = from, To = to, TripId = tripId };
                var result = await connection.QueryAsync<TypeCarSeats>(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<AllrSeatsProcedure>> GetAllSeats(Guid tripId, string from, string to, string typecar)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                // TODO: Rename stored procedures on (GET_ALL_SEATS_BY_TYPECAR)
                var procedure = "[schedule].[GET_ALL_SEATS]";
                var values = new { From = from, To = to, TripId = tripId, TypeCar = typecar };
                var result = await connection.QueryAsync<AllrSeatsProcedure>(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }


        public async Task<IEnumerable<dynamic>> GetRouteInfo(string route)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var procedure = "[route].[ORDERED_STATIONS]";
                var values = new { Route = route };
                var result = await connection.QueryAsync(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}

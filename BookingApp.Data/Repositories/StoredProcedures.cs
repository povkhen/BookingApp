using BookingApp.Data.Entities.Procedure_Models;
using BookingApp.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BookingApp.Data
{
    public class StoredProcedures : IStoredProcedures
    {
        public async Task<IEnumerable<TripSearch>> GetSearchTrips(string departureStation, string arrivalStation, DateTime date)
        {
            using (var connection = ConnectionFactory.CreateConnection())
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
            using (var connection = ConnectionFactory.CreateConnection())
            {
                var procedure = "[schedule].[GET_GROUPING_SEATS]";
                var values = new {From = from, To = to, TripId = tripId };
                var result = await connection.QueryAsync<TypeCarSeats>(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> GetRouteInfo(string route)
        {
            using (var connection = ConnectionFactory.CreateConnection())
            {
                var procedure = "[route].[ORDERED_STATIONS]";
                var values = new { Route = route };
                var result = await connection.QueryAsync(procedure, values, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}

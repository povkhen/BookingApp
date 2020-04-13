using BookingApp.Data.Entities;
using BookingApp.Data.Infrastructure;
using BookingApp.Data.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingApp.Data.Repositories
{
    public class StationRepository : Repository<Station>, IStationRepository
    {
        public StationRepository(string tableName) : base(tableName) { }

        public async Task<Station> FindStationByNameAsync(string name)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Station>($"SELECT * FROM {_tableName} WHERE Name=@Name", new { Name = name });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with name [{name}] could not be found.");
                return result;
            }
        }

        public async Task<Guid?> FindStationIdByNameAsync(string name)
        {
            using (var connection = DBConnection.CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<Guid?>($"SELECT Id FROM {_tableName} WHERE Name=@Name", new { Name = name });
                if (result == null)
                    throw new KeyNotFoundException($"{_tableName} with name [{name}] could not be found.");
                return result;
            }
        }
    }
}

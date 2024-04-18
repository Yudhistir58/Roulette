using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Roulette.Configuration;
using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dapper;
using System.Linq;

namespace Roulette.Repositories
{
    public class ResultRepository : IResultRepository
    {
        private readonly IOptionsSnapshot<ConnectionStringOptions> _connectionStrings;

        public ResultRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<List<ResultModel>> RetrieveAllAsync()
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var results = new List<ResultModel>();
            try
            {
                results = (await sqlConnection.QueryAsync<ResultModel>
                    (sql: "Select * from result")).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return results;
        }
    }
}

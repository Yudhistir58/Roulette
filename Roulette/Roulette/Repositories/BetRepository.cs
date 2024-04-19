using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Roulette.Configuration;
using Roulette.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Roulette.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly IOptionsSnapshot<ConnectionStringOptions> _connectionStrings;

        public BetRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<List<BetModel>> RetrieveAllAsync()
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var bets = new List<BetModel>();
            try
            {
                bets = (await sqlConnection.QueryAsync<BetModel>
                    (sql: "Select * from bet")).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return bets;
        }

        public async Task<List<BetModel>> RetrieveBetAsync(int betId)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var bets = new List<BetModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@betId", betId);
                bets = (await sqlConnection.QueryAsync<BetModel>
                    (sql: "Select * from bet where betId = @betId", param)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return bets;
        }
    }
}

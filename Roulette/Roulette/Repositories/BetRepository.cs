using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Roulette.Configuration;
using Roulette.Controllers;
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
        private readonly ILogger<BetController> _logger;

        public BetRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings, ILogger<BetController> logger)
        {
            _connectionStrings = connectionStrings;
            _logger = logger;
        }

        public async Task<List<BetModel>> RetrieveAllAsync()
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var bets = new List<BetModel>();
            try
            {
                _logger.LogInformation("Attempting RetrieveAllAsync");
                bets = (await sqlConnection.QueryAsync<BetModel>
                    (sql: "Select * from bet")).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"RetrieveAllAsync - {ex}");
            }
            return bets;
        }

        public async Task<List<BetModel>> RetrieveBetAsync(int betId)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var bets = new List<BetModel>();
            try
            {
                _logger.LogInformation("Attempting RetrieveBetAsync");
                var param = new DynamicParameters();
                param.Add("@betId", betId);
                bets = (await sqlConnection.QueryAsync<BetModel>
                    (sql: "Select * from bet where betId = @betId", param)).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"RetrieveBetAsync - {ex}");
            }
            return bets;
        }
    }
}

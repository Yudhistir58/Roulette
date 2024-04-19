using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using Roulette.Configuration;
using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Dapper;
using System.Linq;
using Microsoft.Extensions.Logging;
using Roulette.Controllers;

namespace Roulette.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IOptionsSnapshot<ConnectionStringOptions> _connectionStrings;
        private readonly ILogger<BetController> _logger;

        public PlayerRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings, ILogger<BetController> logger)
        {
            _connectionStrings = connectionStrings;
            _logger = logger;
        }

        public async Task<List<PlayerModel>> RetrieveAllAsync()
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var players = new List<PlayerModel>();
            try
            {
                _logger.LogInformation("Attempting RetrieveAllAsync");
                players = (await sqlConnection.QueryAsync<PlayerModel>
                    (sql: "Select * from player")).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"RetrieveAllAsync - {ex}");
            }
            return players;
        }
    }
}

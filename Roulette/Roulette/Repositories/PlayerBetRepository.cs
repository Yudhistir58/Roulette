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
    public class PlayerBetRepository : IPlayerBetRepository
    {
        private readonly IOptionsSnapshot<ConnectionStringOptions> _connectionStrings;

        public PlayerBetRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<List<PlayerBetModel>> RetrieveAllAsync()
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var playerBets = new List<PlayerBetModel>();
            try
            {
                playerBets = (await sqlConnection.QueryAsync<PlayerBetModel>
                    (sql: "Select * from PlayerBet")).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return playerBets;
        }
    }
}

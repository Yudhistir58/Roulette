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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IOptionsSnapshot<ConnectionStringOptions> _connectionStrings;

        public PlayerRepository(IOptionsSnapshot<ConnectionStringOptions> connectionStrings)
        {
            _connectionStrings = connectionStrings;
        }

        public async Task<List<PlayerModel>> RetrieveAllAsync()
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var players = new List<PlayerModel>();
            try
            {
                players = (await sqlConnection.QueryAsync<PlayerModel>
                    (sql: "Select * from player")).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return players;
        }
    }
}

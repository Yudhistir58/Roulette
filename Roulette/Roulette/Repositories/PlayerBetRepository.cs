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

        public async Task<List<PlayerBetModel>> RetrievePlayerBetAsync(int playerBetId)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var playerBets = new List<PlayerBetModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@playerBetId", playerBetId);
                playerBets = (await sqlConnection.QueryAsync<PlayerBetModel>
                    (sql: "Select * from playerbet where playerbetId = @playerBetId", param)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return playerBets;
        }

        public async Task<bool> UpdatePlayerBetAsync(PlayerBetModel playerBet)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var playerBets = new List<PlayerBetModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@playerBetId", playerBet.PlayerBetId);
                param.Add("@amount", playerBet.Amount);
                param.Add("@status", playerBet.Status);
                param.Add("@payoutValue", playerBet.PayoutValue);
                param.Add("@resultId", playerBet.ResultId);
                await sqlConnection.ExecuteAsync(sql: "update playerbet set amount = @amount, status = @status, payoutValue = @payoutValue, resultId = @resultId where playerbetId = @playerBetId", param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePlayerBetResultAsync(PlayerBetModel playerBet)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var playerBets = new List<PlayerBetModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@playerBetId", playerBet.PlayerBetId);
                param.Add("@amount", playerBet.Amount);
                param.Add("@status", playerBet.Status);
                param.Add("@payoutValue", playerBet.PayoutValue);
                param.Add("@resultId", playerBet.ResultId);
                await sqlConnection.ExecuteAsync(sql: "update playerbet set amount = @amount, status = @status, payoutValue = @payoutValue, resultId = @resultId where playerbetId = @playerBetId", param);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

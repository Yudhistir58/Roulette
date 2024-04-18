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

        public async Task<List<ResultModel>> RetrieveResultAsync(int resultId)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);

            var results = new List<ResultModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@resultId",resultId);
                results = (await sqlConnection.QueryAsync<ResultModel>
                    (sql: "Select * from result where resultId = @resultId", param)).ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
            return results;
        }

        public async Task<ResultModel> GenerateNewResultAsync(ResultModel result)
        {
            using var sqlConnection = new SqliteConnection(_connectionStrings.Value.RouletteDb);
            var newResult = new List<ResultModel>();
            try
            {
                var param = new DynamicParameters();
                param.Add("@resultValue", result.ResultValue);
                param.Add("@resultTime", Convert.ToString(result.ResultTime));
                await sqlConnection.ExecuteAsync(sql: "Insert into result (resultValue, resultTime) values (@resultValue, @resultTime)", param);

                newResult = (await sqlConnection.QueryAsync<ResultModel>(sql: "SELECT last_insert_rowid() as 'ResultId'")).ToList();
                result.ResultId = newResult.Select(r=>r.ResultId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}

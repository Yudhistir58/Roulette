using Microsoft.AspNetCore.Mvc;
using Roulette.Models;
using Roulette.Repositories;
using System;
using System.Threading.Tasks;

namespace Roulette.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;

        public ResultService(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        public async Task<ResultModel> GenerateNewResultAsync()
        {
            Random random = new Random();
            var spinValue = random.Next(0, 37);
            var newResult = new ResultModel() { ResultValue = spinValue, ResultTime = DateTime.Now };
            var createdResult = await _resultRepository.GenerateNewResultAsync(newResult);
            return createdResult;
        }
    }
}

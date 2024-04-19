using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roulette.Controllers;
using Roulette.Models;
using Roulette.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Roulette.Services
{
    public class ResultService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly ILogger<BetController> _logger;

        public ResultService(IResultRepository resultRepository, ILogger<BetController> logger)
        {
            _resultRepository = resultRepository;
            _logger = logger;
        }

        public async Task<ResultModel> GenerateNewResultAsync()
        {
            _logger.LogInformation("Generating spin result");
            var spinValue = GetRandomSpinValue();
            var newResult = new ResultModel() { ResultValue = spinValue, ResultTime = DateTime.Now };
            var createdResult = await _resultRepository.GenerateNewResultAsync(newResult);
            return createdResult;
        }

        private int GetRandomSpinValue()
        {
            Random random = new Random();
            return random.Next(0, 37);
        }

        [Fact]
        public void GetRandomSpinValueTest()
        {
            var spinValue = GetRandomSpinValue();
            Assert.True(spinValue >= 0 && spinValue <= 36);
        }
    }
}

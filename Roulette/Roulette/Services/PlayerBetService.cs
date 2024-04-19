using Roulette.Models;
using System.Threading.Tasks;
using System;
using Roulette.Repositories;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Roulette.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Roulette.Services
{
    public class PlayerBetService : IPlayerBetService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IPlayerBetRepository _playerBetRepository;
        private readonly IBetRepository _betRepository;
        private readonly ILogger<BetController> _logger;
        private readonly List<int> _redValues = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        private readonly List<int> _blackValues = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };

        public PlayerBetService(IPlayerBetRepository playerBetRepository, IResultRepository resultRepository, IBetRepository betRepository, ILogger<BetController> logger)
        {
            _playerBetRepository = playerBetRepository;
            _resultRepository = resultRepository;
            _betRepository = betRepository;
            _logger = logger;
        }
        public async Task<PlayerBetModel> GetPlayerBetResultAsync(int playerBetId, int resultId)
        {
            _logger.LogInformation("Calculating win or loss and payout");
            var result = (await _resultRepository.RetrieveResultAsync(resultId)).FirstOrDefault();
            var playerBet = (await _playerBetRepository.RetrievePlayerBetAsync(playerBetId)).FirstOrDefault();
            var bet = (await _betRepository.RetrieveBetAsync(playerBet.BetId)).FirstOrDefault();
            var win = false;
            if (result != null && playerBet != null && bet != null) 
            {
                win = CheckWinner(result.ResultValue, bet.Number, bet.BetType);
            }
            playerBet.ResultId = resultId;
            playerBet.PayoutValue = win ? playerBet.Amount * bet.Multiplier : 0;
            return playerBet;
        }

        private bool CheckWinner(int resultValue, int number, string betType)
        {
            switch (betType)
            {
                case "Number":
                    return number == resultValue;                    
                case "Odd":
                    return (resultValue % 2) == 1;                    
                case "Even":
                    return (resultValue % 2) == 0;                    
                case "Red":
                    return _redValues.Any(x => x.Equals(resultValue));                   
                case "Black":
                    return _blackValues.Any(x => x.Equals(resultValue));                   
                case "FirstColumn":
                    return (resultValue % 3) == 1;
                case "SecondColumn":
                    return (resultValue % 3) == 2;
                case "ThirdColumn":
                    return (resultValue % 3) == 0;
                case "FirstDozen":
                    return resultValue <= 12;
                case "SecondDozen":
                    return resultValue <= 24 && resultValue > 12;
                case "ThirdDozen":
                    return resultValue <= 36 && resultValue > 24;
                case "FirstHalf":
                    return resultValue <= 18;
                case "SecondHalf":
                    return resultValue <= 36 && resultValue > 18;
                default: 
                    return false;
            }
        }
    }
}

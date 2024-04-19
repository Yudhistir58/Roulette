using Roulette.Models;
using System.Threading.Tasks;
using System;
using Roulette.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace Roulette.Services
{
    public class PlayerBetService : IPlayerBetService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IPlayerBetRepository _playerBetRepository;
        private readonly IBetRepository _betRepository;
        private readonly List<int> _redValues = new List<int>() { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
        private readonly List<int> _blackValues = new List<int>() { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 };
        public PlayerBetService(IPlayerBetRepository playerBetRepository, IResultRepository resultRepository, IBetRepository betRepository)
        {
            _playerBetRepository = playerBetRepository;
            _resultRepository = resultRepository;
            _betRepository = betRepository;
        }
        public async Task<PlayerBetModel> GetPlayerBetResultAsync(int playerBetId, int resultId)
        {
            var result = (await _resultRepository.RetrieveResultAsync(resultId)).FirstOrDefault();
            var playerBet = (await _playerBetRepository.RetrievePlayerBetAsync(playerBetId)).FirstOrDefault();
            var bet = (await _betRepository.RetrieveBetAsync(playerBet.BetId)).FirstOrDefault();
            var win = false;
            if (result != null && playerBet != null && bet != null) 
            {
                switch (bet.BetType) 
                {
                    case "Number": win = bet.Number == result.ResultValue;
                        break;
                    case "Odd": win = (result.ResultValue % 2) == 1; 
                        break;
                    case "Even":
                        win = (result.ResultValue % 2) == 0;
                        break;
                    case "Red":
                        win = _redValues.Any(x=> x.Equals(result.ResultValue));
                        break;
                    case "Black":
                        win = _blackValues.Any(x => x.Equals(result.ResultValue));
                        break;
                    case "FirstColumn":
                        win = (result.ResultValue % 3) == 1;
                        break;
                    case "SecondColumn":
                        win = (result.ResultValue % 3) == 2;
                        break;
                    case "ThirdColumn":
                        win = (result.ResultValue % 3) == 0;
                        break;
                    case "FirstDozen":
                        win = result.ResultValue <= 12;
                        break;
                    case "SecondDozen":
                        win = result.ResultValue <= 24 && result.ResultValue >12;
                        break;
                    case "ThirdDozen":
                        win = result.ResultValue <= 36 && result.ResultValue > 24;
                        break;
                    case "FirstHalf":
                        win = result.ResultValue <= 18;
                        break;
                    case "SecondHalf":
                        win = result.ResultValue <= 36 && result.ResultValue > 18;
                        break;
                }
            }
            playerBet.ResultId = result.ResultId;
            playerBet.PayoutValue = win ? playerBet.Amount * bet.Multiplier : 0;
            return playerBet;
        }
    }
}

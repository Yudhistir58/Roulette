using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Repositories
{
    public interface IPlayerBetRepository
    {
        Task<List<PlayerBetModel>> RetrieveAllAsync();
        Task<List<PlayerBetModel>> RetrievePlayerBetAsync(int playerBetId);
        Task<bool> UpdatePlayerBetAsync(PlayerBetModel playerBet);
        Task<PlayerBetModel> PlaceNewPlayerBetAsync(PlayerBetModel playerBet);
    }
}

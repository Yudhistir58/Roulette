using Roulette.Models;
using System.Threading.Tasks;

namespace Roulette.Services
{
    public interface IPlayerBetService
    {
        Task<PlayerBetModel> GetPlayerBetResultAsync(int playerBetId, int resultId);
    }
}

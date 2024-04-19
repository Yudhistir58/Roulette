using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Repositories
{
    public interface IPlayerRepository
    {
        Task<List<PlayerModel>> RetrieveAllAsync();
    }
}

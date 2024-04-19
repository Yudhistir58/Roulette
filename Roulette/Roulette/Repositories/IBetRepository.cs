using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Repositories
{
    public interface IBetRepository
    {
        Task<List<BetModel>> RetrieveAllAsync();
        Task<List<BetModel>> RetrieveBetAsync(int betId);
    }
}

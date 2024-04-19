using Roulette.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Roulette.Repositories
{
    public interface IResultRepository
    {
        Task<List<ResultModel>> RetrieveAllAsync();
        Task<List<ResultModel>> RetrieveResultAsync(int resultId);
        Task<ResultModel> GenerateNewResultAsync(ResultModel result);
    }
}

using Microsoft.AspNetCore.Mvc;
using Roulette.Models;
using System.Threading.Tasks;

namespace Roulette.Services
{
    public interface IResultService
    {
        Task<ResultModel> GenerateNewResultAsync();
    }
}

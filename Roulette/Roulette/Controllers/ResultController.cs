using Microsoft.AspNetCore.Mvc;
using Roulette.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Roulette.Services;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class ResultController : Controller
    {
        private readonly IResultRepository _resultRepository;
        private readonly IResultService _resultService;

        public ResultController(IResultRepository resultRepository, IResultService resultService)
        {
            _resultRepository = resultRepository;
            _resultService = resultService;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            var results = await _resultRepository.RetrieveAllAsync();
            return Ok(results);
        }

        [HttpGet("{resultId:int}")]
        [ActionName(nameof(RetrieveResultAsync))]
        public async Task<ActionResult> RetrieveResultAsync(int resultId)
        {
            var results = await _resultRepository.RetrieveResultAsync(resultId);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> GenerateNewResultAsync()
        {
            var newResult = await _resultService.GenerateNewResultAsync();
            if (newResult is null)
            {
                return BadRequest();
            }
            return Ok(newResult);
        }
    }
}

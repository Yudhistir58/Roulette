using Microsoft.AspNetCore.Mvc;
using Roulette.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Roulette.Services;
using Microsoft.Extensions.Logging;

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
        private readonly ILogger<BetController> _logger;


        public ResultController(IResultRepository resultRepository, IResultService resultService, ILogger<BetController> logger)
        {
            _resultRepository = resultRepository;
            _resultService = resultService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            _logger.LogInformation("Retriving all Results");
            var results = await _resultRepository.RetrieveAllAsync();
            return Ok(results);
        }

        [HttpGet("{resultId:int}")]
        [ActionName(nameof(RetrieveResultAsync))]
        public async Task<ActionResult> RetrieveResultAsync(int resultId)
        {
            _logger.LogInformation("Retriving Specific Result");
            var results = await _resultRepository.RetrieveResultAsync(resultId);
            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult> GenerateNewResultAsync()
        {
            _logger.LogInformation("Generating new Result");
            var newResult = await _resultService.GenerateNewResultAsync();
            if (newResult is null)
            {
                return BadRequest();
            }
            return Ok(newResult);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Roulette.Repositories;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class BetController : Controller
    {
        private readonly IBetRepository _betRepository;
        private readonly ILogger<BetController> _logger;

        public BetController(IBetRepository betRepository, ILogger<BetController> logger)
        {
            _betRepository = betRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            _logger.LogInformation("Retriving all Bets");
            var bets = await _betRepository.RetrieveAllAsync();
            return Ok(bets);
        }

        [HttpGet("betId:int")]
        public async Task<ActionResult> RetrieveBetAsync(int betId)
        {
            _logger.LogInformation("Retriving specific Bet");
            var bets = await _betRepository.RetrieveBetAsync(betId);
            return Ok(bets);
        }
    }
}

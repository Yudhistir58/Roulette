using Microsoft.AspNetCore.Mvc;
using Roulette.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ILogger<BetController> _logger;

        public PlayerController(IPlayerRepository playerRepository, ILogger<BetController> logger)
        {
            _playerRepository = playerRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            _logger.LogInformation("Retriving all Players");
            var players = await _playerRepository.RetrieveAllAsync();
            return Ok(players);
        }
    }
}

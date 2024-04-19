using Microsoft.AspNetCore.Mvc;
using Roulette.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Roulette.Models;
using Roulette.Services;
using Microsoft.Extensions.Logging;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class PlayerBetController : Controller
    {
        private readonly IPlayerBetRepository _playerBetRepository;
        private readonly IPlayerBetService _playerBetService;
        private readonly ILogger<BetController> _logger;

        public PlayerBetController(IPlayerBetRepository playerBetRepository, IPlayerBetService playerBetService, ILogger<BetController> logger)
        {
            _playerBetRepository = playerBetRepository;
            _playerBetService = playerBetService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            _logger.LogInformation("Retriving all PlayerBets");
            var bets = await _playerBetRepository.RetrieveAllAsync();
            return Ok(bets);
        }

        [HttpGet("playerBetId:int")]
        public async Task<ActionResult> RetrievePlayerBetAsync(int playerBetId)
        {
            _logger.LogInformation("Retriving Specific PlayerBet");
            var bets = await _playerBetRepository.RetrievePlayerBetAsync(playerBetId);
            return Ok(bets);
        }

        [HttpPut("{playerBetId:int}")]
        public async Task<IActionResult> UpdatePlayerBetAsync(int playerBetId, PlayerBetModel playerBet)
        {           
            if (playerBet.PlayerBetId != playerBetId)
            {
                _logger.LogInformation("Player Bet ID does not match that in updated model");
                return StatusCode(403);
            }

            var playerBetFromDb = await _playerBetRepository.RetrievePlayerBetAsync(playerBetId);

            if (playerBetFromDb is null)
            {
                _logger.LogInformation("Player Bet does not exist");
                return NotFound();
            }

            var succeeded = await _playerBetRepository.UpdatePlayerBetAsync(playerBet);
            return succeeded ? NoContent() : BadRequest();
        }

        [HttpGet("{playerBetId:int}/{resultId:int}")]
        public async Task<PlayerBetModel> GetPlayerBetResultAsync(int playerBetId, int resultId)
        {
            _logger.LogInformation("Retriving Player Bet Result");
            var playerBet = await _playerBetService.GetPlayerBetResultAsync(playerBetId, resultId);
            return playerBet;
        }
    }
}

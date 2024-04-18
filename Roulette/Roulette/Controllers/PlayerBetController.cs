using Microsoft.AspNetCore.Mvc;
using Roulette.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using Roulette.Models;

namespace Roulette.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Consumes(Application.Json)]
    [Produces(Application.Json)]
    public class PlayerBetController : Controller
    {
        private readonly IPlayerBetRepository _playerBetRepository;

        public PlayerBetController(IPlayerBetRepository playerBetRepository)
        {
            _playerBetRepository = playerBetRepository;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            var bets = await _playerBetRepository.RetrieveAllAsync();
            return Ok(bets);
        }

        [HttpGet("playerBetId:int")]
        public async Task<ActionResult> RetrievePlayerBetAsync(int playerBetId)
        {
            var bets = await _playerBetRepository.RetrievePlayerBetAsync(playerBetId);
            return Ok(bets);
        }

        [HttpPut("{playerBetId:int}")]
        public async Task<IActionResult> UpdatePlayerBetAsync(int playerBetId, PlayerBetModel playerBet)
        {
            if (playerBet.PlayerBetId != playerBetId)
            {
                //todo throw error
                return StatusCode(403);
            }

            var playerBetFromDb = await _playerBetRepository.RetrievePlayerBetAsync(playerBetId);

            if (playerBetFromDb is null)
            {
                //todo throw error
                return NotFound();
            }

            var succeeded = await _playerBetRepository.UpdatePlayerBetAsync(playerBet);
            return succeeded ? NoContent() : BadRequest();
        }
    }
}

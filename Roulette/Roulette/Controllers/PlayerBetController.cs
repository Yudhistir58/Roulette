using Microsoft.AspNetCore.Mvc;
using Roulette.Repositories;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;

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
    }
}

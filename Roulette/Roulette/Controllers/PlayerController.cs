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
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerController(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            var players = await _playerRepository.RetrieveAllAsync();
            return Ok(players);
        }
    }
}

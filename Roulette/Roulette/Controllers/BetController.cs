using Microsoft.AspNetCore.Mvc;
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

        public BetController(IBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            var shows = await _betRepository.RetrieveAllAsync();
            return Ok(shows);
        }
    }
}

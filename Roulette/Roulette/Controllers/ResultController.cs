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
    public class ResultController : Controller
    {
        private readonly IResultRepository _resultRepository;

        public ResultController(IResultRepository resultRepository)
        {
            _resultRepository = resultRepository;
        }

        [HttpGet]
        public async Task<ActionResult> RetrieveAllAsync()
        {
            var results = await _resultRepository.RetrieveAllAsync();
            return Ok(results);
        }
    }
}

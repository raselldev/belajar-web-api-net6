using BelajarWebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BelajarWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilmsController : ControllerBase
    {
        private readonly IGhibliApi _ghibliApi;

        public FilmsController(IGhibliApi ghibliApi)
        {
            _ghibliApi = ghibliApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var films = await _ghibliApi.GetFilms();

            return Ok(films);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var film = await _ghibliApi.GetFilm(id);

            return Ok(film);
        }
    }
}

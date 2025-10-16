using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendReactFinal.Services.Interfaces;

namespace BackendReactFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("catalog")]
        public async Task<IActionResult> GetMovieCatalog()
        {
            var result = await _movieService.GetMoviesCatalogWithMetadata();
            return Ok(result);
        }

        [HttpGet("catalog/{id}")]
        public async Task<IActionResult> GetMovieData(int id)
        {
            var result = await _movieService.GetMovieWithMetadata(id);
            return Ok(result);
        }
    }
}

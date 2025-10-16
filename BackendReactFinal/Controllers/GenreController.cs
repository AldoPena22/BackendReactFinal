using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendReactFinal.Model.DTOs.Genre;
using BackendReactFinal.Services.Implementations;
using BackendReactFinal.Services.Interfaces;

namespace BackendReactFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreByIdAsync(id);
            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre([FromBody] GenreCreateDTO dto)
        {
            await _genreService.AddGenreAsync(dto);
            return Ok("Genre created successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreUpdateDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest("Id mismatch");
            }

            await _genreService.UpdateGenreAsync(dto);
            return Ok("Genre updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _genreService.DeleteGenreAsync(id);
            return Ok("Genre deleted successfully.");
        }
    }
}

using BackendReactFinal.Model.DTOs.Genre;
using BackendReactFinal.Model.Entities;

namespace BackendReactFinal.Services.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponseDTO>> GetAllGenresAsync();
        Task<GenreResponseDTO> GetGenreByIdAsync(int id);
        Task AddGenreAsync(GenreCreateDTO dto);
        Task UpdateGenreAsync(GenreUpdateDTO dto);
        Task DeleteGenreAsync(int id);
    }
}

using BackendReactFinal.Model.DTOs.Movie;

namespace BackendReactFinal.Services.Interfaces
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCatalogResponseDTO>> GetMoviesCatalogWithMetadata();
        Task<MovieResponseDTO> GetMovieWithMetadata(int movieId);
    }
}

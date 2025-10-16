using Microsoft.EntityFrameworkCore;
using BackendReactFinal.DAL;
using BackendReactFinal.Model.DTOs.Movie;
using BackendReactFinal.Model.Entities;
using BackendReactFinal.Repositories.Interfaces;
using BackendReactFinal.Services.Interfaces;

namespace BackendReactFinal.Services.Implementations
{
    public class MovieService : IMovieService
    {
        private readonly AppDbContext _context;

        public MovieService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MovieCatalogResponseDTO>> GetMoviesCatalogWithMetadata()
        {
            var movies = await _context.Movies
                .Include(m => m.Ratings)
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .ToListAsync();

            var movieCatalog = movies.Select(movie =>
            {
                var averageRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;
                var genreNames = movie.MovieGenres.Select(mg => mg.Genre.Name).Distinct().ToList();

                return new MovieCatalogResponseDTO
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    PictureUrl = movie.PictureUrl,
                    ReleaseDate = movie.ReleaseDate,
                    AverageRating = Math.Round(averageRating, 2),
                    Genres = genreNames
                };
            }).ToList();

            return movieCatalog;

        }

        public async Task<MovieResponseDTO> GetMovieWithMetadata(int movieId)
        {

            var movie = await _context.Movies
                .Include(m => m.MovieGenres)
                    .ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieActors)
                    .ThenInclude(ma => ma.Actor)
                .Include(m => m.Ratings)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie == null)
            {
                return null;
            }

            var genres = movie.MovieGenres.Select(mg => mg.Genre.Name).Distinct().ToList();

            var actors = movie.MovieActors.Select(ma => new ActorsInfo
            {
                ActorName = ma.Actor?.FullName ?? "Unknown",
                PictureUrl = ma.Actor?.PictureUrl ?? string.Empty
            }).ToList();

            var averageRating = movie.Ratings.Any() ? movie.Ratings.Average(r => r.Score) : 0;

            var individualRatings = movie.Ratings.Select(r => new IndividualRating
            {
                Score = r.Score,
                Comment = r.Comment ?? string.Empty,
                UserName = r.User?.UserName ?? string.Empty
            }).ToList();

            var ratingInfo = new RatingInfo
            {
                AverageRating = Math.Round(averageRating, 2),
                IndividualRating = individualRatings
            };

            return new MovieResponseDTO
            {
                MovieId = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                PictureUrl = movie.PictureUrl,
                ReleaseDate = movie.ReleaseDate,
                Genres = genres,
                Actors = actors,
                Rating = new List<RatingInfo> { ratingInfo }
            };

        }
    }
}

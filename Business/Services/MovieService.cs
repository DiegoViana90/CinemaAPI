using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using CinemaApi.Validators;
using System.Threading.Tasks;
using CinemaApi.DTOs.Response;

namespace CinemaApi.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository;

        public MovieService(IMovieRepository movieRepository, IRoomRepository roomRepository)
        {
            _movieRepository = movieRepository;
            _roomRepository = roomRepository;
        }

        public async Task InsertNewMovie(InsertMovieRequest insertMovieRequest)
        {
            await Validator.ValidateInsertMovieRequestAsync(insertMovieRequest, _roomRepository, _movieRepository);

            Movie movie = new Movie
            {
                Name = insertMovieRequest.Name,
                Director = insertMovieRequest.Director,
                Duration = insertMovieRequest.Duration,
                RoomId = insertMovieRequest.RoomNumber,
            };

            await _movieRepository.InsertNewMovie(movie);
        }

          public async Task<MovieResponse> GetMovieByName(string name)
        {
            Validator.ValidateGetMovieByNameRequest(name); 

            var movie = await _movieRepository.GetMovieByName(name);
            if (movie == null)
            {
                throw new KeyNotFoundException("Filme não encontrado.");
            }
            
            return new MovieResponse
            {
                Name = movie.Name,
                Director = movie.Director,
                Duration = movie.Duration,
                RoomNumber = movie.RoomId
            };
        }
    }
}

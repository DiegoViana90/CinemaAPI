using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using CinemaApi.Validators;
using System.Threading.Tasks;

namespace CinemaApi.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IRoomRepository _roomRepository; // Adicionando a dependência

        public MovieService(IMovieRepository movieRepository, IRoomRepository roomRepository)
        {
            _movieRepository = movieRepository;
            _roomRepository = roomRepository; // Inicializando a dependência
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
    }
}

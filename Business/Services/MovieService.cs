using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using CinemaApi.Repositories.Interface;
using CinemaApi.Validators;
using CinemaAPI.Models;

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

            Room room = null;
            if (!string.IsNullOrEmpty(insertMovieRequest.RoomNumber))
            {
                room = await _roomRepository.GetRoomByNumber(insertMovieRequest.RoomNumber);
            }

            Movie movie = new Movie
            {
                Name = insertMovieRequest.Name,
                Director = insertMovieRequest.Director,
                Duration = insertMovieRequest.Duration,
                RoomId = room?.RoomId,
            };

            await _movieRepository.InsertNewMovie(movie);
        }

        public async Task<MovieResponse> GetMovieByName(string name)
        {
            Validator.ValidateGetMovieByNameRequest(name);
            
            Movie movie = await _movieRepository.GetMovieByName(name);
            if (movie == null)
            {
                throw new KeyNotFoundException("Filme não encontrado.");
            }

            return new MovieResponse
            {
                Name = movie.Name,
                Director = movie.Director,
                Duration = movie.Duration,
                RoomNumber = movie.Room?.RoomNumber,
                Description = movie.Room?.Description
            };
        }

        public async Task<MovieResponse> UpdateMovie(UpdateMovieRoomRequest updateMovieRoomRequest)
        {
            await Validator.ValidateUpdateMovieRoomRequestAsync(updateMovieRoomRequest, _roomRepository, _movieRepository);

            Movie movie = await _movieRepository.GetMovieByName(updateMovieRoomRequest.Name);
            Room room = await _roomRepository.GetRoomByNumber(updateMovieRoomRequest.RoomNumber);

            movie.RoomId = room?.RoomId;

            await _movieRepository.UpdateMovie(movie);

            return new MovieResponse
            {
                Name = movie.Name,
                Director = movie.Director,
                Duration = movie.Duration,
                RoomNumber = room?.RoomNumber
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            return await _movieRepository.GetAllMovies();
        }
    }
}

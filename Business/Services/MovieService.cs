using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var movie = new Movie
            {
                Name = insertMovieRequest.Name,
                Director = insertMovieRequest.Director,
                Duration = insertMovieRequest.Duration,
                MovieRooms = new List<MovieRoom>()
            };

            if (!string.IsNullOrEmpty(insertMovieRequest.RoomNumber))
            {
                var room = await _roomRepository.GetRoomByNumber(insertMovieRequest.RoomNumber);
                movie.MovieRooms.Add(new MovieRoom { Movie = movie, Room = room });
            }

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
                RoomNumbers = movie.MovieRooms.Select(mr => mr.Room.RoomNumber).ToList(),
                Descriptions = movie.MovieRooms.Select(mr => mr.Room.Description).ToList()
            };
        }

        public async Task<MovieResponse> UpdateMovie(UpdateMovieRoomRequest updateMovieRoomRequest)
        {
            await Validator.ValidateUpdateMovieRoomRequestAsync(updateMovieRoomRequest, _roomRepository, _movieRepository);

            var movie = await _movieRepository.GetMovieByName(updateMovieRoomRequest.Name);

            var room = await _roomRepository.GetRoomByNumber(updateMovieRoomRequest.RoomNumber);
            var existingMovieRoom = movie.MovieRooms.FirstOrDefault(mr => mr.RoomId == room.RoomId);
            
            if (existingMovieRoom == null)
            {
                movie.MovieRooms.Add(new MovieRoom { MovieId = movie.MovieId, RoomId = room.RoomId });
            }

            await _movieRepository.UpdateMovie(movie);

            return new MovieResponse
            {
                Name = movie.Name,
                Director = movie.Director,
                Duration = movie.Duration,
                RoomNumbers = movie.MovieRooms.Select(mr => mr.Room.RoomNumber).ToList(),
                Descriptions = movie.MovieRooms.Select(mr => mr.Room.Description).ToList()
            };
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            IEnumerable<MovieResponse> movies = await _movieRepository.GetAllMovies();
            return movies;
        }

        public async Task RemoveMovieFromRoom(string movieName, string roomNumber)
        {
            await Validator.ValidateRemoveMovieFromRoomRequest(movieName, roomNumber, _roomRepository, _movieRepository);
            await _movieRepository.RemoveMovieFromRoom(movieName, roomNumber);
        }
    }
}

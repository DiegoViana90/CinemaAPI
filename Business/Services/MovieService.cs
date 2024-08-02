using CinemaApi.Business.Interface;
using CinemaApi.DTOs;
using CinemaApi.DTOs.Request;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using CinemaApi.Validators;

namespace CinemaApi.Business.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task InsertNewMovie(InsertMovieRequest insertMovieRequest)
        {       
            Validator.ValidateInsertMovieRequest(insertMovieRequest);
            
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

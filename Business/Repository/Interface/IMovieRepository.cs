using System.Collections.Generic;
using System.Threading.Tasks;
using CinemaApi.DTOs.Response;
using CinemaAPI.Models;

namespace CinemaApi.Repositories.Interface
{
    public interface IMovieRepository
    {
        Task InsertNewMovie(Movie movie);
        Task<bool> MovieExists(string name);
        Task<bool> MovieExists(string name, string roomNumber);
        Task<Movie> GetMovieByName(string name);
        Task UpdateMovie(Movie movie);
        Task<IEnumerable<MovieResponse>> GetAllMovies();
        Task RemoveMovieFromRoom(string movieName, string roomNumber);
    }
}

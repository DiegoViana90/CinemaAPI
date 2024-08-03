using CinemaApi.DTOs.Response;
using CinemaAPI.Models;
using System.Threading.Tasks;

namespace CinemaApi.Repositories.Interface
{
    public interface IMovieRepository
    {
        Task InsertNewMovie(Movie movie);
        Task<bool> MovieExists(string name, string roomNumber); 
        Task<Movie> GetMovieByName(string name);
        Task UpdateMovie(Movie movie);
        Task<IEnumerable<MovieResponse>> GetAllMovies();
    }
}

using CinemaAPI.Models;
using System.Threading.Tasks;

namespace CinemaApi.Repositories.Interface
{
    public interface IMovieRepository
    {
        Task InsertNewMovie(Movie movie);
        Task<bool> MovieExists(string name);
        Task<Movie> GetMovieByName(string name);
    }
}

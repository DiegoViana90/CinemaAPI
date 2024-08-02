using CinemaAPI.Models;

namespace CinemaApi.Repositories.Interface
{
    public interface IMovieRepository
    {
        Task InsertNewMovie(Movie movie);
    }
}

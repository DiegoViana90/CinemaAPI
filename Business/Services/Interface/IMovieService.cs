using CinemaApi.DTOs;
using CinemaApi.DTOs.Request;

namespace CinemaApi.Business.Interface
{
    public interface IMovieService
    {
        Task InsertNewMovie(InsertMovieRequest insertMovieRequest);
    }
}

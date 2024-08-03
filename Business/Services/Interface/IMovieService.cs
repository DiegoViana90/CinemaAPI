using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;

namespace CinemaApi.Business.Interface
{
    public interface IMovieService
    {
        Task InsertNewMovie(InsertMovieRequest insertMovieRequest);
        Task<MovieResponse> GetMovieByName(string name);
        Task<MovieResponse> UpdateMovie(UpdateMovieRoomRequest updateMovieRoomRequest);
        Task<IEnumerable<MovieResponse>> GetAllMovies(); 
    }
}

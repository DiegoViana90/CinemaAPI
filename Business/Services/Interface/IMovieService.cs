using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaApi.Business.Interface
{
    public interface IMovieService
    {
        Task InsertNewMovie(InsertMovieRequest insertMovieRequest);
        Task<MovieResponse> GetMovieByName(string name);
    }
}

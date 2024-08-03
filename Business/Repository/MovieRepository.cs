using CinemaApi.Data;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaApi.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;

        public MovieRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task InsertNewMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> MovieExists(string name)
        {
             var exists = await _context.Movies.AnyAsync(m => m.Name == name);
             return exists;
        }
    }
}

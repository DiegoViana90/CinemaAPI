using CinemaApi.Data;
using CinemaApi.DTOs.Response;
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

        public async Task<bool> MovieExists(string name, string roomNumber)
        {
            bool movieExists = await _context.Movies
                                 .Include(m => m.Room)
                                 .AnyAsync(m => m.Name == name && m.Room.RoomNumber == roomNumber);
            return movieExists;
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            Movie movie = await _context.Movies
                                 .Include(m => m.Room)
                                 .FirstOrDefaultAsync(m => m.Name == name);
            return movie;
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            IEnumerable<MovieResponse> movies = await _context.Movies
                                 .Include(m => m.Room)
                                 .Select(m => new MovieResponse
                                 {
                                     Name = m.Name,
                                     Director = m.Director,
                                     Duration = m.Duration,
                                     RoomNumber = m.Room.RoomNumber,
                                     Description = m.Room.Description
                                 })
                                 .ToListAsync();
            return movies;
        }

        public async Task<bool> MovieExistsInRoom(string name, int roomId)
        {
            bool movieExistsInRoom = await _context.Movies.AnyAsync(m => m.Name == name && m.RoomId == roomId);
            return movieExistsInRoom;
        }
    }
}

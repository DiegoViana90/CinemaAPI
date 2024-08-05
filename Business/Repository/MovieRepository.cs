using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinemaApi.Data;
using CinemaApi.DTOs.Response;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> MovieExists(string name)
        {
            bool movieExists = await _context.Movies.AnyAsync(m => m.Name == name);
            return movieExists;
        }

        public async Task<bool> MovieExists(string name, string roomNumber)
        {
            bool movieExists = await _context.MovieRooms
                .Include(mr => mr.Movie)
                .Include(mr => mr.Room)
                .AnyAsync(mr => mr.Movie.Name == name && mr.Room.RoomNumber == roomNumber);

            return movieExists;
        }

        public async Task<Movie> GetMovieByName(string name)
        {
            Movie movie = await _context.Movies
                .Include(m => m.MovieRooms)
                .ThenInclude(mr => mr.Room)
                .FirstOrDefaultAsync(m => m.Name == name);

            return movie;
        }

        public async Task UpdateMovie(Movie movie)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Movies.Update(movie);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }

        public async Task<IEnumerable<MovieResponse>> GetAllMovies()
        {
            IEnumerable<MovieResponse> movies = await _context.Movies
                .Include(m => m.MovieRooms)
                .ThenInclude(mr => mr.Room)
                .Select(m => new MovieResponse
                {
                    Name = m.Name,
                    Director = m.Director,
                    Duration = m.Duration,
                    RoomNumbers = m.MovieRooms.Select(mr => mr.Room.RoomNumber).ToList(),
                    Descriptions = m.MovieRooms.Select(mr => mr.Room.Description).ToList()
                })
                .ToListAsync();

            return movies;
        }

        public async Task RemoveMovieFromRoom(string movieName, string roomNumber)
        {
            var movieRoom = await _context.MovieRooms
                .Include(mr => mr.Movie)
                .Include(mr => mr.Room)
                .FirstOrDefaultAsync(mr => mr.Movie.Name == movieName && mr.Room.RoomNumber == roomNumber);

            if (movieRoom != null)
            {
                _context.MovieRooms.Remove(movieRoom);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UnscheduleMovie(string movieName)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Name == movieName);

                        if (movie != null)
                        {
                            var movieRooms = _context.MovieRooms.Where(mr => mr.MovieId == movie.MovieId);
                            _context.MovieRooms.RemoveRange(movieRooms);
                            await _context.SaveChangesAsync();
                        }

                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            });
        }
    }
}

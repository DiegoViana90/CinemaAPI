using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }

        public DbSet<Room> Room { get; set; }
        public DbSet<Movie> Movie { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasKey(r => r.RoomNumber);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Movies)
                .WithOne(m => m.Room)
                .HasForeignKey(m => m.RoomNumber);
        }
    }
}

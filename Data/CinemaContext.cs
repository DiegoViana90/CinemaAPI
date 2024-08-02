using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaApi.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasKey(r => r.RoomId);

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Movies)
                .WithOne(m => m.Room)
                .HasForeignKey(m => m.RoomId);

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MovieId);

            modelBuilder.Entity<Movie>()
                .Property(m => m.MovieId)
                .ValueGeneratedOnAdd();
        }
    }
}

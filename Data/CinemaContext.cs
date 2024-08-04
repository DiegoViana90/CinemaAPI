using Microsoft.EntityFrameworkCore;
using CinemaAPI.Models;

namespace CinemaApi.Data
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions<CinemaContext> options) : base(options) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieRoom> MovieRooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Room>()
                .HasKey(r => r.RoomId);

            modelBuilder.Entity<Room>()
                .HasIndex(r => r.RoomNumber)
                .IsUnique();

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MovieId);

            modelBuilder.Entity<Movie>()
                .Property(m => m.MovieId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<MovieRoom>()
                .HasKey(mr => new { mr.MovieId, mr.RoomId });

            modelBuilder.Entity<MovieRoom>()
                .HasOne(mr => mr.Movie)
                .WithMany(m => m.MovieRooms)
                .HasForeignKey(mr => mr.MovieId);

            modelBuilder.Entity<MovieRoom>()
                .HasOne(mr => mr.Room)
                .WithMany(r => r.MovieRooms)
                .HasForeignKey(mr => mr.RoomId);
        }
    }
}

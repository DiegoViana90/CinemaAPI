namespace CinemaAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        public int? RoomNumber { get; set; }
        public Room Room { get; set; }
    }
}
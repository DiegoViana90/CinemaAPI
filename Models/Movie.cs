namespace CinemaAPI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}

namespace CinemaAPI.Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        public int? RoomId { get; set; }
        public Room? Room { get; set; }
    }
}

namespace CinemaAPI.Models
{
    public class MovieRoom
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}

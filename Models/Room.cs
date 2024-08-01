namespace CinemaAPI.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
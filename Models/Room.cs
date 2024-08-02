namespace CinemaAPI.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string? Description { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}

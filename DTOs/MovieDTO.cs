namespace CinemaApi.DTOs
{
    public class MovieDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Director { get; set; } = string.Empty;
        public TimeSpan Duration { get; set; }
        public int? RoomNumber { get; set; }
    }
}

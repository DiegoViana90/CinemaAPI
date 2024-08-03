namespace CinemaApi.DTOs.Response
{
    public class MovieResponse
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        public string? RoomNumber { get; set; }
        public string? Description { get; set; } 
    }
}

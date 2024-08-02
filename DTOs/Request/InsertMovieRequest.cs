namespace CinemaApi.DTOs.Request
{
    public class InsertMovieRequest
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        public int? RoomNumber { get; set; } 
    }
}
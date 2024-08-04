
using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Request
{
    public class InsertMovieRequest
    {
        public string Name { get; set; }
        public string Director { get; set; }
        public TimeSpan Duration { get; set; }
        
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "O número da sala deve conter exatamente 3 dígitos numéricos.")]
        public string? RoomNumber { get; set; } 
    }
}
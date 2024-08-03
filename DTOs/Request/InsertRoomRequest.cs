using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Request
{
    public class InsertRoomRequest
    {
        [Required(ErrorMessage = "O número da sala é obrigatório.")]
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "O número da sala deve conter exatamente 3 dígitos.")]
        public string RoomNumber { get; set; }

        public string? Description { get; set; }
    }
}

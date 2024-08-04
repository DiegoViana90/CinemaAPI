using System.ComponentModel.DataAnnotations;

namespace CinemaApi.DTOs.Request
{
    public class UpdateRoomRequest
    {
        [RegularExpression("^[0-9]{3}$", ErrorMessage = "O número da sala deve conter exatamente 3 dígitos numéricos.")]
        public string RoomNumber { get; set; }
        public string Description { get; set; }
    }
}

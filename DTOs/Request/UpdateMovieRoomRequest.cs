using System.ComponentModel.DataAnnotations;

public class UpdateMovieRoomRequest
{
    public string Name { get; set; }
    
    [RegularExpression("^[0-9]{3}$", ErrorMessage = "O número da sala deve conter exatamente 3 dígitos numéricos.")]
    public string? RoomNumber { get; set; }
}

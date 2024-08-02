using System;
using CinemaApi.DTOs.Request;

namespace CinemaApi.Validators
{
    public class Validator
    {
        public static bool ValidateInsertMovieRequest(InsertMovieRequest insertMovieRequest)
        {
            if (insertMovieRequest == null)
            {
                throw new ArgumentNullException(nameof(insertMovieRequest), "O request não pode ser nulo.");
            }

            if (string.IsNullOrEmpty(insertMovieRequest.Name))
            {
                throw new ArgumentException("O nome do filme não pode ser vazio.");
            }

            if (string.IsNullOrEmpty(insertMovieRequest.Director))
            {
                throw new ArgumentException("O nome do diretor não pode ser vazio.");
            }

            if (insertMovieRequest.Duration <= TimeSpan.Zero)
            {
                throw new ArgumentException("A duração do filme deve ser maior que zero.");
            }

            if (insertMovieRequest.RoomNumber <= 0)
            {
                throw new ArgumentException("O número da sala deve ser maior que zero.");
            }

            return true;
        }
    }
}

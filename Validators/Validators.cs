using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CinemaApi.DTOs.Request;
using CinemaApi.Repositories.Interface;

namespace CinemaApi.Validators
{
    public static class Validator
    {
        public static void ValidateGetMovieByNameRequest(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("O nome do filme não pode ser vazio.");
            }
        }

        public static async Task ValidateInsertRoomRequestAsync(InsertRoomRequest insertRoomRequest, IRoomRepository roomRepository)
        {
            if (insertRoomRequest == null)
            {
                throw new ArgumentNullException(nameof(insertRoomRequest), "O request não pode ser nulo.");
            }

            if (string.IsNullOrEmpty(insertRoomRequest.RoomNumber))
            {
                throw new ArgumentException("O número da sala não pode ser vazio.");
            }

            if (!Regex.IsMatch(insertRoomRequest.RoomNumber, "^[0-9]{3}$"))
            {
                throw new ArgumentException("O número da sala deve conter exatamente 3 dígitos.");
            }

            bool roomExists = await roomRepository.RoomExistsByNumber(insertRoomRequest.RoomNumber);
            if (roomExists)
            {
                throw new ArgumentException("Uma sala com este número já existe.");
            }
        }

        public static async Task ValidateInsertMovieRequestAsync(InsertMovieRequest insertMovieRequest,
            IRoomRepository roomRepository, IMovieRepository movieRepository)
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

            if (!string.IsNullOrEmpty(insertMovieRequest.RoomNumber) && !Regex.IsMatch(insertMovieRequest.RoomNumber, "^[0-9]{3}$"))
            {
                throw new ArgumentException("O número da sala deve conter exatamente 3 dígitos.");
            }

            if (!string.IsNullOrEmpty(insertMovieRequest.RoomNumber))
            {
                var room = await roomRepository.GetRoomByNumber(insertMovieRequest.RoomNumber);
                if (room == null)
                {
                    throw new KeyNotFoundException("Sala não encontrada.");
                }

                bool movieExists = await movieRepository.MovieExists(insertMovieRequest.Name, insertMovieRequest.RoomNumber);
                if (movieExists)
                {
                    throw new ArgumentException("Um filme com este nome já existe na sala especificada.");
                }
            }
        }

        public static async Task ValidateUpdateMovieRoomRequestAsync(UpdateMovieRoomRequest updateMovieRoomRequest,
            IRoomRepository roomRepository, IMovieRepository movieRepository)
        {
            if (updateMovieRoomRequest == null)
            {
                throw new ArgumentNullException(nameof(updateMovieRoomRequest), "O request não pode ser nulo.");
            }

            if (string.IsNullOrEmpty(updateMovieRoomRequest.Name))
            {
                throw new ArgumentException("O nome do filme não pode ser vazio.");
            }

            if (!string.IsNullOrEmpty(updateMovieRoomRequest.RoomNumber) && !Regex.IsMatch(updateMovieRoomRequest.RoomNumber, "^[0-9]{3}$"))
            {
                throw new ArgumentException("O número da sala deve conter exatamente 3 dígitos.");
            }

            if (!string.IsNullOrEmpty(updateMovieRoomRequest.RoomNumber))
            {
                var room = await roomRepository.GetRoomByNumber(updateMovieRoomRequest.RoomNumber);
                if (room == null)
                {
                    throw new KeyNotFoundException("Sala não encontrada.");
                }

                bool movieExists = await movieRepository.MovieExists(updateMovieRoomRequest.Name, updateMovieRoomRequest.RoomNumber);
                if (movieExists)
                {
                    throw new ArgumentException("Um filme com este nome já existe na sala especificada.");
                }
            }

            bool movieExistsOverall = await movieRepository.MovieExists(updateMovieRoomRequest.Name, updateMovieRoomRequest.RoomNumber);
            if (!movieExistsOverall)
            {
                throw new KeyNotFoundException("Filme não encontrado.");
            }
        }
    }
}

using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using CinemaApi.Repositories.Interface;
using CinemaApi.Validators;
using CinemaAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaApi.Business.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task InsertNewRoom(InsertRoomRequest insertRoomRequest)
        {
            await Validator.ValidateInsertRoomRequestAsync(insertRoomRequest, _roomRepository);

            Room room = new Room
            {
                RoomNumber = insertRoomRequest.RoomNumber,
                Description = insertRoomRequest.Description
            };

            await _roomRepository.InsertNewRoom(room);
        }

        public async Task<IEnumerable<RoomResponse>> GetAllRooms()
        {
            var rooms = await _roomRepository.GetAllRooms();
            return rooms.Select(r => new RoomResponse
            {
                RoomNumber = r.RoomNumber,
                Description = r.Description
            });
        }

        public async Task<RoomResponse> GetRoomByNumber(string roomNumber)
        {
            var room = await _roomRepository.GetRoomByNumber(roomNumber);
            if (room == null)
            {
                throw new KeyNotFoundException("Sala não encontrada.");
            }

            return new RoomResponse
            {
                RoomNumber = room.RoomNumber,
                Description = room.Description
            };
        }

        public async Task UpdateRoom(UpdateRoomRequest updateRoomRequest)
        {
            var room = await _roomRepository.GetRoomByNumber(updateRoomRequest.RoomNumber);
            if (room == null)
            {
                throw new KeyNotFoundException("Sala não encontrada.");
            }

            room.Description = updateRoomRequest.Description;

            await _roomRepository.UpdateRoom(room);
        }

        public async Task DeleteRoom(string roomNumber)
        {
            var room = await _roomRepository.GetRoomByNumber(roomNumber);
            if (room == null)
            {
                throw new KeyNotFoundException("Sala não encontrada.");
            }

            await _roomRepository.DeleteRoom(room);
        }
    }
}

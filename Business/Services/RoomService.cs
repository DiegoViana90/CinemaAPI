using CinemaApi.Business.Interface;
using CinemaApi.DTOs.Request;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using CinemaApi.Validators;
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
    }
}

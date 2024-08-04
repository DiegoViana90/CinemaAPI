using CinemaApi.DTOs.Request;
using CinemaApi.DTOs.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CinemaApi.Business.Interface
{
    public interface IRoomService
    {
        Task InsertNewRoom(InsertRoomRequest insertRoomRequest);
        Task<IEnumerable<RoomResponse>> GetAllRooms();
        Task<RoomResponse> GetRoomByNumber(string roomNumber);
        Task UpdateRoom(UpdateRoomRequest updateRoomRequest);
        Task DeleteRoom(string roomNumber);
    }
}

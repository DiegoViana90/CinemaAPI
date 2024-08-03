using CinemaAPI.Models;
using System.Threading.Tasks;

namespace CinemaApi.Repositories.Interface
{
    public interface IRoomRepository
    {
        Task InsertNewRoom(Room room);
        Task<bool> RoomExists(int roomId);
        Task<bool> RoomExistsByNumber(string roomNumber); 
    }
}

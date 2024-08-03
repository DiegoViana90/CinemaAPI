using CinemaApi.DTOs.Request;
using CinemaAPI.Models;
using System.Threading.Tasks;

namespace CinemaApi.Business.Interface
{
    public interface IRoomService
    {
        Task InsertNewRoom(InsertRoomRequest insertRoomRequest);
    }
}

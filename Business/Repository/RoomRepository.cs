using CinemaApi.Data;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaApi.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly CinemaContext _context;

        public RoomRepository(CinemaContext context)
        {
            _context = context;
        }

        public async Task InsertNewRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RoomExists(int roomId)
        {
            bool roomExists = await _context.Rooms.AnyAsync(r => r.RoomId == roomId);
            return roomExists;
        }

        public async Task<bool> RoomExistsByNumber(string roomNumber)
        {
            bool roomExists =  await _context.Rooms.AnyAsync(r => r.RoomNumber == roomNumber);
            return roomExists;
        }
    }
}

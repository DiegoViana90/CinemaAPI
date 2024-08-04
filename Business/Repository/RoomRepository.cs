using System.Collections.Generic;
using System.Threading.Tasks;
using CinemaApi.Business.Services;
using CinemaApi.Data;
using CinemaApi.Repositories.Interface;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> RoomExistsByNumber(string roomNumber)
        {
            bool roomExists = await _context.Rooms.AnyAsync(r => r.RoomNumber == roomNumber);
            return roomExists;
        }

        public async Task<Room> GetRoomByNumber(string roomNumber)
        {
            Room room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
            return room;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            IEnumerable<Room> rooms = await _context.Rooms.ToListAsync();
            return rooms;
        }

        public async Task UpdateRoom(Room room)
        {
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoom(Room room)
        {
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}

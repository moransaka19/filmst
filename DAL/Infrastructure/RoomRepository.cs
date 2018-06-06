using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    public class RoomRepository : IRepository<Room>
    {
        FilmStDBContext _db;

        public RoomRepository(FilmStDBContext db)
        {
            _db = db;
        }

        public void Create(Room item)
        {
            _db.Rooms.Add(item);
        }

        public Room Get(string key)
        {
            return _db.Rooms.Find(key);
        }

        public IEnumerable<Room> GetAll()
        {
            return _db.Rooms;
        }

        public void Remove(Room item)
        {
            Room room = _db.Rooms.Find(item.Name);
            if (room!=null)
            {
                _db.Rooms.Remove(room);
            }
        }

        public void AddTrack(Room room,Track track)
        {
            Room FindRoom = _db.Rooms.Find(room.Name);
            if (FindRoom !=null)
            {
                FindRoom.TracksIn.Add(track);
            }
        }

        public void Update(Room item)
        {
            _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}

using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;

namespace DAL.Infrastructure
{
    public class TrackRepository : IRepository<Track>
    {
        FilmStDBContext _db;
        public TrackRepository(FilmStDBContext db)
        {
            _db = db;
        }

        public void Create(Track item)
        {
            _db.Tracks.Add(item);
        }

        public Track Get(string key)
        {
            return _db.Tracks.Find(key);
        }

        public IEnumerable<Track> GetAll()
        {
            return _db.Tracks;
        }

        public void Remove(Track item)
        {
            Track track = _db.Tracks.Find(item.TrackName);
            if (track != null)
            {
                _db.Tracks.Remove(track);
            }
        }

        public void Update(Track item)
        {
            _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}

using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    public class UnitOfWork : IDisposable
    {
        RoomRepository _roomRepository;
        UserRepository _userRepository;
        TrackRepository _trackRepository;
        FilmStDBContext _db;

        public UnitOfWork(string connectionString)
        {
            _db = new FilmStDBContext(connectionString);
        }

        public RoomRepository Rooms
        {
            get
            {
                if (_roomRepository == null)
                {
                    _roomRepository = new RoomRepository(_db);
                }
                return _roomRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_db);
                }
                return _userRepository;
            }
        }

        public TrackRepository Tracks
        {
            get
            {
                if (_trackRepository == null)
                {
                    _trackRepository = new TrackRepository(_db);
                }
                return _trackRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }


        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

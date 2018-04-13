using DAL.Identity;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    class UnitOfWork : IDisposable
    {
        RoomRepository _roomRepository;
        UserRepository _userRepository;
        DBContextSrv _db;

        public UnitOfWork(string connectionString)
        {
            _db = new DBContextSrv(connectionString);
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

using DAL.Identity;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    class UserRepository : IRepository<ApplicationUser>
    {
        DBContextSrv _db;
        public UserRepository(DBContextSrv db)
        {
            _db = db;
        }

        public void Create(ApplicationUser item)
        {
            _db.Users.Add(item);
        }

        public ApplicationUser Get(string key)
        {
            return _db.Users.Find(key);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _db.Users;
        }

        public void Remove(ApplicationUser item)
        {
            ApplicationUser user = _db.Users.Find(item.Email);
            if (user!=null)
            {
                _db.Users.Remove(user);
            }
        }

        public void Update(ApplicationUser item)
        {
            _db.Entry(item).State = System.Data.Entity.EntityState.Modified;
        }
    }
}

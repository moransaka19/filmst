using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Infrastructure
{
    public class UserRepository : IRepository<ApplicationUser>
    {
        FilmStDBContext _db;
        public UserRepository(FilmStDBContext db)
        {
            _db = db;
        }

        public void Create(ApplicationUser item)
        {
            _db.ApplicationUsers.Add(item);
        }

        public ApplicationUser Get(string key)
        {
            return _db.ApplicationUsers.Find(key);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _db.ApplicationUsers;
        }

        public void Remove(ApplicationUser item)
        {
            ApplicationUser user = _db.ApplicationUsers.Find(item.Email);
            if (user!=null)
            {
                _db.ApplicationUsers.Remove(user);
            }
        }

        public void Update(ApplicationUser item)
        {
            _db.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;
using System.Data.Entity;

namespace DAL.Context
{
    public class FilmStDBContext : DbContext
    {
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public FilmStDBContext(string connection): base(connection)
            { }
    }
}

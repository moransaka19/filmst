using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL
{
    public class FilmStDBContext : DbContext
    {
        public DbSet<Track> Trackes { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public FilmStDBContext()
            : base()
        {
        }
    }
}

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DAL.Identity
{
    class DBContextSrv : DbContext
    {
        public DBContextSrv(string connectionString) : base(connectionString) { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}

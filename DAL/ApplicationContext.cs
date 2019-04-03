using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	class ApplicationContext : DbContext
	{
		public DbSet<Message> Messages { get; set; }
		public DbSet<PlayList> PlayLists { get; set; }
		public DbSet<Room> Rooms { get; set; }

		public ApplicationContext(DbContextOptions options) 
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Room>()
				.HasOne(r => r.PlayList)
				.WithOne(pl => pl.Room)
				.HasForeignKey<PlayList>(pl => pl.RoomId);
		}
	}
}

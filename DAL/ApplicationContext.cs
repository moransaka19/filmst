using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
	public class ApplicationContext : IdentityDbContext<User, IdentityRole<long>, long>
	{
		public DbSet<Message> Messages { get; set; }
		public DbSet<PlayList> PlayLists { get; set; }
		public DbSet<Room> Rooms { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options) 
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Room>()
				.HasOne(r => r.PlayList)
				.WithOne(pl => pl.Room)
				.HasForeignKey<PlayList>(pl => pl.RoomId);

			modelBuilder.Entity<UserRoom>()
				.HasKey(ur => new { ur.RoomId, ur.UserId });

			modelBuilder.Entity<UserRoom>()
				.HasOne(ur => ur.Room)
				.WithMany(r => r.UserRooms)
				.HasForeignKey(ur => ur.RoomId);

			modelBuilder.Entity<UserRoom>()
				.HasOne(ur => ur.User)
				.WithMany(u => u.UserRooms)
				.HasForeignKey(ur => ur.UserId);
		}
	}
}

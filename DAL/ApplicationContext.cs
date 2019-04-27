using System;
using DAL.Entities;
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
			#region Relationships

			modelBuilder.Entity<Room>()
				.HasOne(r => r.PlayList)
				.WithOne(pl => pl.Room)
				.HasForeignKey<PlayList>(pl => pl.RoomId);

			modelBuilder.Entity<Room>()
				.HasMany(r => r.Messages)
				.WithOne(m => m.Room)
				.HasForeignKey(m => m.RoomId);

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

			#endregion

			#region DataSeeding
			
			var user = new User()
			{
				Id = 1,
				UserName = "admin",
				NormalizedUserName = "admin".ToUpper()
			};

			user.PasswordHash = new PasswordHasher<User>().HashPassword(user, "Password1");

			modelBuilder.Entity<User>()
				.HasData(user);

			modelBuilder.Entity<IdentityRole<long>>()
				.HasData(new IdentityRole<long> { Id = 1, Name = "Admin", NormalizedName = "Admin".ToUpper() });

			modelBuilder.Entity<UserRoom>()
				.HasData(new UserRoom { UserId = 1, RoomId = 1 });
			
			modelBuilder.Entity<Message>()
				.HasData(new Message()
				{
					Id = 1,
					DateSent = DateTime.UtcNow,
					RoomId = 1,
					UserId = 1,
					HashMessage = "SomeMessage"
				});

			modelBuilder.Entity<Room>()
				.HasData(new Room()
				{
					Id = 1,
					Name = "Room1",
					UniqName = "UniqRoomNameAzaza"
				});

			modelBuilder.Entity<PlayList>()
				.HasData(new PlayList()
				{
					Id = 1,
					RoomId = 1,
					TrackCurrentTime = new TimeSpan()
				});

			#endregion

			base.OnModelCreating(modelBuilder);
		}
	}
}

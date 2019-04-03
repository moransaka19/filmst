using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using SharedKernel.Abstractions.DAL.Models;

namespace DAL.Entities
{
	public class Room : BaseEntity, IRoom
	{
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public string UniqName { get; set; }
		public long HostId { get; set; }

		public long PlayListId { get; set; }
		public PlayList PlayList { get; set; }
		public IEnumerable<UserRoom> UserRooms { get; set; }

		[NotMapped]
		IEnumerable<IUserRoom> IRoom.UserRooms
		{
			get => UserRooms.Select(u => u as IUserRoom);
			set => UserRooms = value.Select(u => u as UserRoom);
		}

		[NotMapped]
		IPlayList IRoom.PlayList
		{
			get => PlayList;
			set => PlayList = value as PlayList;
		}
	}
}

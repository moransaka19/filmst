using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel.Models;

namespace DAL.Entities
{
	public class Message : BaseEntity
	{
		public long UserId { get; set; }
		public User User { get; set; }
		public string HashMessage { get; set; }
		public DateTime DateSent { get; set; }
		public long RoomId { get; set; }
		public Room Room { get; set; }
	}
}

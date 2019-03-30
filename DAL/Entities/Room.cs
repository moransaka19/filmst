using SharedKernel.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entities
{
	class Room : BaseEntity
	{
		public string Name { get; set; }
		public string PasswordHash { get; set; }
		public string UniqName { get; set; }
		public long HostId { get; set; }

		public long PlayListId { get; set; }
		public PlayList PlayList { get; set; }

		[NotMapped]
		public IEnumerable<User> Users { get; set; }
	}
}

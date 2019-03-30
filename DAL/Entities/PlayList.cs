using System;
using System.Collections.Generic;
using System.Text;
using SharedKernel.Models;

namespace DAL.Entities
{
	class PlayList : BaseEntity
	{
		public long RoomId { get; set; }
		public Room Room { get; set; }

		public TimeSpan TrackCurrentTime { get; set; }

	}
}

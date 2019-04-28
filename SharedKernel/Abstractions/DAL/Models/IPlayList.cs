using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Abstractions.DAL.Models
{
	public interface IPlayList
	{
		long RoomId { get; set; }
		IRoom Room { get; set; }

		IEnumerable<IMedia> Medias { get; set; }
		TimeSpan TrackCurrentTime { get; set; }
	}
}

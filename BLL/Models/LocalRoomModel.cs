using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace BLL.Models
{
	class LocalRoomModel
	{
		public ICollection<long> UserIds { get; set; }
		public ICollection<Media> Medias { get; set; }

		public LocalRoomModel()
		{
			UserIds = new List<long>();
			Medias = new List<Media>();
		}
	}
}

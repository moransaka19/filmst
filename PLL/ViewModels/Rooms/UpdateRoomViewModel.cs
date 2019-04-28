using System.Collections.Generic;
using SharedKernel.Abstractions.PLL.Rooms.Models;

namespace PLL.ViewModels.Rooms
{
	public class UpdateRoomViewModel : IUpdateRoomViewModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }
		public string UniqName { get; set; }

		public long PlayListId { get; set; }
		public IEnumerable<long> UserIds { get; set; }
	}
}

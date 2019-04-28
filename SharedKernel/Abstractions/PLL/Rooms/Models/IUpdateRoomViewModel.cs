using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Abstractions.PLL.Rooms.Models
{
	public interface IUpdateRoomViewModel
	{
		long Id { get; set; }
		string Name { get; set; }
		string Password { get; set; }
		string UniqName { get; set; }
		
		long PlayListId { get; set; }
		IEnumerable<long> UserIds { get; set; }
	}
}

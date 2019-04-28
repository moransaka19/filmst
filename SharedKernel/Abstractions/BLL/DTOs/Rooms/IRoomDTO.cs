using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Abstractions.BLL.DTOs.Rooms
{
	public interface IRoomDTO
	{
		string Name { get; set; }
		string UniqName { get; set; }
		long HostId { get; set; }
		
		IEnumerable<long> UserIds { get; set; }
	}
}

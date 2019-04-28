using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Abstractions.BLL.DTOs.Rooms
{
	public interface IAddRoomDTO
	{
		string Name { get; set; }
		string Password { get; set; }
		string UniqName { get; set; }
		long HostId { get; set; }
	}
}

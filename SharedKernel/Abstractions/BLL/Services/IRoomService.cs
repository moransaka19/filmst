using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel.Abstractions.BLL.DTOs.Rooms;

namespace SharedKernel.Abstractions.BLL.Services
{
	public interface IRoomService
	{
		Task AddAsync(IAddRoomDTO dto);
		Task SignInAsync(ISignInRoomDTO dto);
		string GetRoomName();
		void DisconnectFromRoom();
		Task AddToRoomAsync(string roomName, string userName);
	}
}

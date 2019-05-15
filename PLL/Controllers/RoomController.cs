using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SharedKernel.Abstractions.BLL.DTOs.Rooms;
using SharedKernel.Abstractions.BLL.Services;
using SharedKernel.Abstractions.PLL.Rooms;
using SharedKernel.Abstractions.PLL.Rooms.Models;

namespace PLL.Controllers
{
	public class RoomController : IRoomController
	{
		private readonly IRoomService _roomService;

		public RoomController(IRoomService roomService)
		{
			_roomService = roomService;
		}

		public async Task AddAsync(IAddRoomViewModel model)
		{
			await _roomService.AddAsync(Mapper.Map<IAddRoomDTO>(model));
		}
		
		public async Task SignInAsync(ISignInRoomViewModel model)
		{
			await _roomService.SignInAsync(Mapper.Map<ISignInRoomDTO>(model));
		}

		public async Task AddToRoomAsync(string roomName, string userName)
		{
			await _roomService.AddToRoomAsync(roomName, userName);
		}

		public string GetRoomName()
		{
			return _roomService.GetRoomName();
		}

		public void DisconnectFromRoom()
		{
			_roomService.DisconnectFromRoom();
		}
	}
}

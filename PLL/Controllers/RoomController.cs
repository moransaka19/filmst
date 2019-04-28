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

		public async Task UpdateAsync(IUpdateRoomViewModel model)
		{
			await _roomService.UpdateAysnc(Mapper.Map<IUpdateRoomDTO>(model));
		}
	}
}

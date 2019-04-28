using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Abstractions.BLL.DTOs.Rooms;
using SharedKernel.Abstractions.BLL.Services;
using SharedKernel.Abstractions.DAL.Repositories;

namespace BLL.Services
{
	public class RoomService : IRoomService
	{
		private readonly HttpContext _httpContext;
		private readonly UserManager<User> _userManager;
		private readonly IRepository<Room> _roomRepository;

		public RoomService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IRepository<Room> roomRepository)
		{
			_httpContext = httpContextAccessor.HttpContext;
			_userManager = userManager;
			_roomRepository = roomRepository;
		}

		public async Task AddAsync(IAddRoomDTO dto)
		{
			var user = await _userManager.FindByNameAsync(_httpContext.User.Identity.Name);

			var room = Mapper.Map<Room>(dto);

			room.HostId = user.Id;

			_roomRepository.Add(room);

			room.UserRooms = new List<UserRoom>() { new UserRoom() { UserId = room.HostId, RoomId = room.Id } };

			_roomRepository.Update(room);
		}

		public async Task UpdateAysnc(IUpdateRoomDTO dto)
		{
			var user = await _userManager.FindByNameAsync(_httpContext.User.Identity.Name);
			var room = _roomRepository.FindById(dto.Id);

			// TODO: Hande not permitted action
			if (user.Id != room.HostId)
				throw new Exception();

			room = Mapper.Map(dto, room);

			_roomRepository.Update(room);
		}
	}
}

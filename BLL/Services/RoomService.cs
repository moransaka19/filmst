using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using SharedKernel.Abstractions.BLL.DTOs.Rooms;
using SharedKernel.Abstractions.BLL.Services;
using SharedKernel.Abstractions.DAL.Repositories;
using SharedKernel.Exceptions;
using SharedKernel.Helpers;

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

			room.UserRooms = new List<UserRoom>() { new UserRoom(room.HostId, room.Id) };

			_roomRepository.Update(room);
		}

		public async Task SignInAsync(ISignInRoomDTO dto)
		{
			var room = _roomRepository.GetAll(r => r.UniqName == dto.UniqName).FirstOrDefault();

			if (room == null)
				throw new RoomNotFoundException();

			if (room.UserRooms.Any(ur => ur.User.NormalizedUserName == _httpContext.User.Identity.Name.ToUpper()))
			{
				AddRoomToUserIdentity(dto.UniqName);
				return;
			}

			if (room.PasswordHash != HashPasswordHelper.GetPasswordHash(dto.Password))
				throw new IncorrectPasswordException();

			var user = await _userManager.FindByNameAsync(_httpContext.User.Identity.Name);

			room.UserRooms = room.UserRooms.Concat(new UserRoom[] { new UserRoom(user.Id, room.Id) });

			_roomRepository.Update(room);

			AddRoomToUserIdentity(dto.UniqName);
		}

		private void AddRoomToUserIdentity(string roomName)
		{
			_httpContext.User.AddIdentity(new ClaimsIdentity(new Claim[] { new Claim("room", roomName) }));
		}

		public string GetRoom()
		{
			return _roomRepository
				.GetAll(r => r.UserRooms.Any(ur => ur.User.NormalizedUserName == _httpContext.User.Identity.Name.ToUpper()))
				.Select(r => r.UniqName)
				.FirstOrDefault();
		}

		public async Task UpdateAysnc(IUpdateRoomDTO dto)
		{
			var user = await _userManager.FindByNameAsync(_httpContext.User.Identity.Name);
			var room = _roomRepository.FindById(dto.Id);

			if (user.Id != room.HostId)
				throw new UserIsNotHostException();

			room = Mapper.Map(dto, room);

			_roomRepository.Update(room);
		}
	}
}

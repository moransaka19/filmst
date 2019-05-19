using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using PLL.ViewModels;
using SharedKernel.Abstractions.BLL.DTOs.Media;
using SharedKernel.Abstractions.PLL.Rooms;
using SharedKernel.Exceptions;
using SharedKernel.Extensions;

namespace Filmst.Controllers
{
	[Authorize]
	public class RoomHub : Hub
	{
		private readonly IRoomController _roomController;

		private string _roomName => Context.User.FindFirstValue("room");

		public RoomHub(IRoomController roomController)
		{
			_roomController = roomController;
		}

		public override async Task OnConnectedAsync()
		{
			var room = _roomController.GetRoomName();

			if (room == null)
			{
				_roomController.DisconnectFromRoom();
				throw new UserIsNotInTheRoomException();
			}

			await _roomController.AddToRoomAsync(room, Context.ConnectionId);

			await Groups.AddToGroupAsync(Context.ConnectionId, room);

			Context.User.AddIdentity(new ClaimsIdentity(new[] { new Claim("room", room), }));

			await Clients.Group(room).SendAsync("UserConnected", Context.User.Identity.Name);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			await Clients.Group(_roomName).SendAsync("UserDisconnected", Context.User.Identity.Name);

			_roomController.DisconnectFromRoom();
		}

		public async Task CheckMedia(IEnumerable<MediaViewModel> medias)
		{
			var requiredMedias = _roomController.CheckMedia(_roomName, Mapper.Map<IEnumerable<IMediaDTO>>(medias));

			var hostConnectionId = _roomController.GetHostConnectionId();

			if (!requiredMedias.IsNullOrEmpty())
				await Clients.Client(hostConnectionId)
							 .SendAsync("UploadMedia", 
										Mapper.Map<IEnumerable<MediaViewModel>>(requiredMedias));
		}

		public async Task Message(string message)
		{
			await Clients.Group(_roomName).SendAsync("Receive", Context.User.Identity.Name, message);
		}

		public async Task Play()
		{
			await Clients.Group(_roomName).SendAsync("Play");
		}

		public async Task Pause()
		{
			await Clients.Group(_roomName).SendAsync("Pause");
		}

		public async Task Stop()
		{
			await Clients.Group(_roomName).SendAsync("Stop");
		}
	}
}

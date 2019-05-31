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
using PLL.ViewModels.Media;
using SharedKernel.Abstractions.BLL.DTOs.Media;
using SharedKernel.Abstractions.PLL.Media;
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
				await _roomController.DisconnectFromRoomAsync();
				throw new UserIsNotInTheRoomException();
			}

			Context.User.AddIdentity(new ClaimsIdentity(new[] { new Claim("room", room), }));

			await _roomController.AddToRoomAsync(room, Context.ConnectionId);

			await Groups.AddToGroupAsync(Context.ConnectionId, room);

			await Clients.Group(room).SendAsync("UserConnected", Context.User.Identity.Name);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			// True if the room has already closed
			if (_roomController.GetRoomName() == null)
				return;

			await Clients.Group(_roomName).SendAsync("UserDisconnected", Context.User.Identity.Name);

			if (_roomController.GetHostConnectionId() != Context.ConnectionId)
			{
				await Groups.RemoveFromGroupAsync(Context.ConnectionId, _roomController.GetRoomName());
				await _roomController.DisconnectFromRoomAsync();

				if (_roomController.IsAllUsersReadyToStart(_roomName))
					await Clients.Group(_roomName).SendAsync("ReadyToPlay");

				return;
			}

			await Clients.Group(_roomName).SendAsync("RoomClosed");

			var userIds = _roomController.GetUserConnectionIdsInCurrentRoom();

			userIds.ToList().ForEach(id =>
			{
				Groups.RemoveFromGroupAsync(id, _roomName);
			});

			// When the admin disconnects from the room, the room will be closed
			await _roomController.DisconnectFromRoomAsync();
		}

		public async Task SetPlaylist(IEnumerable<IMediaViewModel> medias)
		{
			_roomController.SetPlaylist(medias);

			await Clients.Group(_roomName).SendAsync("RequireCheckMedia");
		}

		public async Task CheckMedia(IEnumerable<MediaViewModel> medias)
		{
			var requiredMedias = _roomController.CheckMedia(_roomName, Mapper.Map<IEnumerable<IMediaDTO>>(medias));

			var hostConnectionId = _roomController.GetHostConnectionId();

			if (!requiredMedias.IsNullOrEmpty())
				await Clients.Client(hostConnectionId)
							 .SendAsync("RequireMedia",
										Mapper.Map<IEnumerable<MediaViewModel>>(requiredMedias), Context.ConnectionId);
			else
				await Clients.Group(_roomName).SendAsync("ReadyToPlay");
		}

		public async Task UploadMedia(string connectionId, string fileName, List<string> сhunks)
		{
			await Clients.Client(connectionId).SendAsync("DownloadMedia", fileName, сhunks);
		}

		public async Task MediaDownloaded()
		{
			_roomController.MediaDownloaded(_roomName);

			if (_roomController.IsAllUsersReadyToStart(_roomName))
			{
				await Clients.Caller.SendAsync("NextMedia", _roomController.GetCurrentMedia(_roomName));

				await Clients.Group(_roomName).SendAsync("ReadyToPlay");
			}
		}

		public async Task Message(string message)
		{
			await Clients.Group(_roomName).SendAsync("Receive", Context.User.Identity.Name, message);
		}

		public async Task SetNextMedia(MediaViewModel media)
		{
			if (Context.ConnectionId != _roomController.GetHostConnectionId())
				return;

			_roomController.NextMedia(_roomName, Mapper.Map<IMediaDTO>(media));

			await Clients.Group(_roomName).SendAsync("NextMedia", media);
		}

		public async Task TrackEnded()
		{
			_roomController.TrackEnded(_roomName);

			if (_roomController.IsAllUsersWaitingOnNextTrack(_roomName))
				await Clients.Client(_roomController.GetHostConnectionId()).SendAsync("RequireNextMedia");
		}

		public async Task Play(TimeSpan currentTrackTime)
		{
			if (Context.ConnectionId != _roomController.GetHostConnectionId())
				return;

			_roomController.TrackStarted(_roomName);

			await Clients.Group(_roomName).SendAsync("Play", currentTrackTime);
		}

		public async Task Pause()
		{

			if (Context.ConnectionId != _roomController.GetHostConnectionId())
				return;
			
			await Clients.Group(_roomName).SendAsync("Pause");
		}

		public async Task Stop()
		{
			if (Context.ConnectionId != _roomController.GetHostConnectionId())
				return;

			await Clients.Group(_roomName).SendAsync("Stop");
		}
	}
}

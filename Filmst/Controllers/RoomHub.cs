using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions.PLL.Rooms;
using SharedKernel.Exceptions;
using SharedKernel.Extensions;

namespace Filmst.Controllers
{
	[Authorize]
	public class RoomHub : Hub
	{
		private readonly IRoomController _roomController;

		private string _room => Context.User.FindFirstValue("room");


		public RoomHub(IRoomController roomController)
		{
			_roomController = roomController;
		}

		public override async Task OnConnectedAsync()
		{
			var room = _roomController.GetRoomName();

			if (room == null)
				throw new UserIsNotInTheRoomException();

			await Groups.AddToGroupAsync(Context.ConnectionId, room);

			Context.User.AddIdentity(new ClaimsIdentity(new Claim[] { new Claim("room", room), }));

			await Clients.Group(room).SendAsync("UserConnected", Context.User.Identity.Name);
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			//_roomController.DisconnectFromRoom();

			await Clients.Group(_room).SendAsync("UserDisconnected", Context.User.Identity.Name);
		}

		public async Task Message(string message)
		{
			await Clients.Group(_room).SendAsync("Receive", Context.User.Identity.Name, message);
		}

		public async Task Play()
		{
			await Clients.Group(_room).SendAsync("Play");
		}

		public async Task Pause()
		{
			await Clients.Group(_room).SendAsync("Pause");
		}

		public async Task Stop()
		{
			await Clients.Group(_room).SendAsync("Stop");
		}
	}
}

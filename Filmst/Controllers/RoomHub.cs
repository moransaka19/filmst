using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Abstractions.PLL.Rooms;
using SharedKernel.Extensions;

namespace Filmst.Controllers
{
	[Authorize]
	public class RoomHub : Hub
	{
		public RoomHub(IRoomController roomController)
		{
			
		}	

		public override async Task OnConnectedAsync()
		{
			Context.User.AddIdentity(new ClaimsIdentity(new Claim[] {new Claim("room", "room1"), }));
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			throw new NotImplementedException();
		}

		public async Task Message(string message)
		{

			var a = Context.User.FindFirst("room");
			await Clients.OthersInGroup(a.Value).SendAsync("Receive", Context.User.Identity.Name, message);
		}

		public async Task Play()
		{
			await Clients.All.SendAsync("Play");
		}

		public async Task Pause()
		{
			await Clients.All.SendAsync("Pause");
		}

		public async Task Stop()
		{
			await Clients.All.SendAsync("Stop");
		}
	}
}

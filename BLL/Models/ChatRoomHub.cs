using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Models
{
	class ChatRoomHub : Hub
    {
        static List<ApplicationUser> Users = new List<ApplicationUser>();

        public void Send(string name, string message)
        {

            Clients.All.addMessage(name, message);
        }

        public void Connect(string userName)
        {
            var id = Int32.Parse(Context.ConnectionId);


            if (!Users.Any(x => x.Id == id))
            {
                Users.Add(new ApplicationUser { Id = id, Email = userName });

                Clients.Caller.onConnected(id, userName, Users);

                Clients.AllExcept(id.ToString()).onNewUserConnected(id, userName);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.Id == Int32.Parse(Context.ConnectionId));
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Email);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}

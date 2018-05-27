using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Models
{
    class ChatRoomHub : Hub
    {
        static List<IdentityUser> Users = new List<IdentityUser>();

        public void Send(string name, string message)
        {

            Clients.All.addMessage(name, message);
        }

        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


            if (!Users.Any(x => x.Id == id))
            {
                Users.Add(new IdentityUser { Id = id, Email = userName });

                Clients.Caller.onConnected(id, userName, Users);

                Clients.AllExcept(id).onNewUserConnected(id, userName);
            }
        }

        public override System.Threading.Tasks.Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.Id == Context.ConnectionId);
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

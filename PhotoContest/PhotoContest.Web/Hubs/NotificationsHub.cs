using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PhotoContest.Web.Hubs
{
    using System.Threading.Tasks;
    using Controllers;
    using Data.Contracts;
    using Microsoft.AspNet.Identity;

    public class NotificationsHub : Hub
    {

        private readonly static ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;

            _connections.Add(name, Context.ConnectionId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }

        public void SendNotifications(string reciever, string message)
        {
            var sender = Context.User.Identity.Name;

            foreach (var connectionId in _connections.GetConnections(reciever))
            {
                Clients.Client(connectionId).receiveNotification();
            }
        }


    }
}
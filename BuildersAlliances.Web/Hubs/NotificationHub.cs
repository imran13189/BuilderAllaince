using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace BuildersAlliances.Web.Hubs
{
    public class NotificationHub : Hub
    {
        public IHubCallerConnectionContext<dynamic> CurrentCaller;
        public NotificationHub()
        {
            CurrentCaller = Clients;
        }
        public void connect()
        {
            Clients.All.getconnectionid(Context.ConnectionId);
        }
    }
}
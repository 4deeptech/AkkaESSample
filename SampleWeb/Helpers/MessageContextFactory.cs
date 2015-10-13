using Actors;
using Akka.EventStore.Cqrs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;

namespace SampleWeb.Helpers
{
    public static class MessageContextFactory
    {
        public static IMessageContext CreateLocal()
        {
            string currentUser = Environment.UserName;
            string ipAddress = GetLocalIPAddress();
            return new MessageContext(currentUser, ipAddress);
        }

        public static IMessageContext Create(HttpContextBase context)
        {
            string currentUser = string.IsNullOrEmpty(context.User.Identity.Name) ? "anonymous" : context.User.Identity.Name.ToLower();
            string ipAddress = context.Request.UserHostAddress;
            return new MessageContext(currentUser, ipAddress);
        }

        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }

    

    
}
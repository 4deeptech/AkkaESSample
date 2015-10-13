using Akka.EventStore.Cqrs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
    public class MessageContext : IMessageContext
    {
        public Dictionary<string, string> Metadata { get; private set; }

        public MessageContext(string userName, string ipAddress)
        {
            Metadata = new Dictionary<string, string>();

            Metadata.Add("Username", userName);
            Metadata.Add("IpAddress", ipAddress);
        }

        public MessageContext(Dictionary<string, string> metadata)
        {
            Metadata = metadata;
        }
    }
}

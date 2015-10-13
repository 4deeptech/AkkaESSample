using Akka.EventStore.Cqrs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
    public static class ContextHelper
    {
        public static IMessageContext CreateFromCommand(Command command)
        {
            return new MessageContext(command.Metadata);
        }
    }


}

using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleWeb.Actors
{
    public static class SystemActors
    {
        public static IActorRef SignalRActor = ActorRefs.Nobody;
        public static IActorRef CommandProcessor = ActorRefs.Nobody;
    }
}
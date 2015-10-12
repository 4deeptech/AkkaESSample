using Akka.Actor;
using Akka.TestKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actor.Tests
{
    /// <summary>
    /// Used to relay messages wired to event sink out to probe for unit tests
    /// </summary>
    public class ProbeRelay : ReceiveActor
    {
        public ProbeRelay(TestProbe probe)
        {
            ReceiveAny(o => 
                {
                    probe.Forward(o);
                });
        }
    }
}

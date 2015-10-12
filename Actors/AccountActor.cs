using Akka.Actor;
using Akka.Event;
using Akka.EventStore.Cqrs.Core;
using Akka.EventStore.Cqrs.Core.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
    public class AccountActor : AggregateRootActor
    {
        private ILoggingAdapter _log;
        private AccountState _state;

        public AccountActor(IAggregateRootCreationParameters parameters)
            : base(parameters)
        {
            _log = Context.GetLogger();
            _state = new AccountState(parameters.Id,this);
        }

        protected override bool Handle(ICommand command)
        {
            _state.Handle(command);
            return true;
        }

        protected override bool Apply(IEvent @event)
        {
            _state.Mutate(@event);
            return true;
        }

        protected override bool RecoverState(object state)
        {
            if (state.CanHandle<AccountState>(x =>
            {
                x.Events = this;
                _state = x;
            }))
                return true;

            return false;
        }

        protected override object GetState()
        {
            return _state;
        }

        public override string PersistenceId
        {
            get { return _state.PersistenceId; }
        }
    }
}

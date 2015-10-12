using Akka.EventStore.Cqrs.Core;
using Akka.EventStore.Cqrs.Core.Domain;
using Domain;
using Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Actors
{
    internal class AccountState : IPersistable
    {
        internal IEventSink Events { get; set; }

        private int Version { get; set; }
        
        private bool Created { get; set; }

        private Account _account = null;

        public AccountState(Guid id,IEventSink events)
        {
            _account = new Account();
            _account.AccountId = id;
            Events = events;
        }

        #region HANDLERS

        public void Handle(CreateAccount msg)
        {
            if (Created)
                throw new DomainException(string.Format("Account {0:n} already exists.", msg.AggregateId));

            Events.Publish(new AccountCreated(msg.AggregateId, msg.TaxNumber, msg.EntityName, msg.Type, Version + 1));
        }

        public void Apply(AccountCreated e)
        {
            Created = true;
            
            _account.AccountId = e.AccountId;
            _account.EntityName = e.EntityName;
            _account.TaxNumber = e.TaxNumber;
            _account.AccountType = e.Type;
            Version = e.Version;
        }

        public void Handle(UpdateAccount msg)
        {
            if (!Created)
                throw new DomainException(string.Format("Account {0:n} does not exist.", msg.AggregateId));

            Events.Publish(new AccountUpdated(msg.AggregateId, msg.TaxNumber, msg.EntityName, msg.Type, Version + 1));
        }

        public void Apply(AccountUpdated e)
        {
            _account.EntityName = e.EntityName;
            _account.TaxNumber = e.TaxNumber;
            _account.AccountType = e.Type;
            Version = e.Version;
        }

        public void Handle(UpdateAccountMailingAddress msg)
        {
            if (!Created)
                throw new DomainException(string.Format("Account {0:n} does not exist.", msg.AggregateId));

            Events.Publish(new AccountMailingAddressUpdated(msg.AggregateId, msg.MailingAddress, Version + 1));
        }

        public void Apply(AccountMailingAddressUpdated e)
        {
            _account.MailingAddress = e.MailingAddress;
            Version = e.Version;
        }

        #endregion

        #region DYNAMIC INVOKERS

        public void Handle(ICommand command)
        {
            ((dynamic)this).Handle((dynamic)command);
        }

        public void Mutate(IEvent @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
        }

        #endregion

        public string PersistenceId
        {
            get { return this._account != null ? this._account.AccountId.ToString() : Guid.Empty.ToString(); }
        }
    }
}

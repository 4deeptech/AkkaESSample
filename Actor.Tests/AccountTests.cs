using Akka;
using Akka.DI.Core;
using Akka.DI.AutoFac;
using Akka.TestKit;
using Akka.TestKit.Xunit2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Autofac;
using Akka.EventStore.Cqrs.Core;
using Xunit.Abstractions;
using Akka.Actor;
using Actors;
using Domain;
using Messages;


namespace Actor.Tests
{
    public class AccountTests : TestKit
    {
        private Guid accountId = Guid.NewGuid();
        private Guid accountId2 = Guid.NewGuid();
        private AggregateRootCreationParameters _parms;

        public AccountTests()
        {
        }

        private TestActorRef<AccountActor> PrepareTest(Guid id,TestProbe probe,string name)
        {
            var relayProps = Props.Create<ProbeRelay>(probe);
            var testSink = Sys.ActorOf(relayProps, "testrelay" + name);
            _parms = new AggregateRootCreationParameters(id,testSink, 4);
            return ActorOfAsTestActorRef(() => new AccountActor(_parms), "test" + name);
        }

        [Fact]
        public void Create_Account()
        {
            var probe = CreateTestProbe("probe");
            var accountRef = PrepareTest(accountId,probe, "1");
            var command = new CreateAccount(accountId, "123456", "TestAccount", AccountType.Individual);
            accountRef.Tell(command);
            probe.ExpectMsg<AccountCreated>(new AccountCreated(accountId, "123456", "TestAccount", AccountType.Individual, 1));
        }

        [Fact]
        public void Update_Account()
        {
            var probe = CreateTestProbe("probe");
            var accountRef = PrepareTest(accountId, probe, "1");
            var commandCreate = new CreateAccount(accountId, "123456", "TestAccount", AccountType.Individual);
            accountRef.Tell(commandCreate);
            probe.ExpectMsg<AccountCreated>(new AccountCreated(accountId, "123456", "TestAccount", AccountType.Individual, 1));
            var commandUpdate = new UpdateAccount(accountId, "123456", "Test Account", AccountType.Individual);
            accountRef.Tell(commandUpdate);
            probe.ExpectMsg<AccountUpdated>(new AccountUpdated(accountId, "123456", "Test Account", AccountType.Individual, 2));
        }

        [Fact]
        public void Update_Account_Mailing_Address()
        {
            var probe = CreateTestProbe("probe");
            var accountRef = PrepareTest(accountId, probe, "1");
            var commandCreate = new CreateAccount(accountId, "123456", "TestAccount", AccountType.Individual);
            accountRef.Tell(commandCreate);
            probe.ExpectMsg<AccountCreated>(new AccountCreated(accountId, "123456", "TestAccount", AccountType.Individual, 1));
            var commandUpdate = new UpdateAccountMailingAddress(accountId, new Domain.Address("123 4TH ST", null, "Oklahoma City", "OK", "73120"));
            accountRef.Tell(commandUpdate);
            probe.ExpectMsg<AccountMailingAddressUpdated>(new AccountMailingAddressUpdated(accountId, new Domain.Address("123 4TH ST", null, "Oklahoma City", "OK", "73120"), 2));
        }

        [Fact]
        public void Create_And_Update_Multiple_PropertyAccounts()
        {
            Guid acc1 = Guid.NewGuid();
            Guid acc2 = Guid.NewGuid();
            var probe = CreateTestProbe("probe1");
            var probe2 = CreateTestProbe("probe2");
            var accountRef = PrepareTest(acc1,probe, "1");
            var accountRef2 = PrepareTest(acc2,probe2, "2");
            
            var command = new CreateAccount(acc1, "123456", "TestAccount", AccountType.Individual);
            accountRef.Tell(command);
            probe.ExpectMsg<AccountCreated>(new AccountCreated(acc1, "123456", "TestAccount", AccountType.Individual, 1));
            var command2 = new CreateAccount(acc2, "923456", "TestAccount2", AccountType.Individual);
            accountRef2.Tell(command2);
            probe2.ExpectMsg<AccountCreated>(new AccountCreated(acc2, "923456", "TestAccount2", AccountType.Individual, 1));

            var commandUpdate = new UpdateAccountMailingAddress(acc1,
                new Domain.Address("123 4TH ST", null, "Oklahoma City", "OK", "73120"));
            accountRef.Tell(commandUpdate);
            probe.ExpectMsg<AccountMailingAddressUpdated>(
                new AccountMailingAddressUpdated(acc1, new Domain.Address("123 4TH ST", null, "Oklahoma City", "OK", "73120"), 2));

            var commandUpdate2 = new UpdateAccountMailingAddress(acc2, new Domain.Address("123 4TH ST", null, "Oklahoma City", "TX", "73120"));
            accountRef2.Tell(commandUpdate2);
            probe2.ExpectMsg<AccountMailingAddressUpdated>(
                new AccountMailingAddressUpdated(acc2, new Domain.Address("123 4TH ST", null, "Oklahoma City", "TX", "73120"), 2));

        }
    }
}

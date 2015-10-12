using Akka.Actor;
using Akka.Routing;
using Grotto.Core.Actor.Actors;
using SampleWeb.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Akka.DI.Core;
using Akka.DI.AutoFac;
using Autofac;
using Akka.EventStore.Cqrs.Core;
using Actors;

namespace SampleWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected static ActorSystem ActorSystem;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var builder = new ContainerBuilder();
            //builder.Register(c => new AggregateRootCreationParameters(ActorRefs.Nobody,4)).As<IAggregateRootCreationParameters>();
            //builder.RegisterType<AccountActor>();
            //var container = builder.Build();
            ActorSystem = ActorSystem.Create("sampleweb");
            //Akka.DI.Core.IDependencyResolver resolver = new AutoFacDependencyResolver(container, ActorSystem);

            
            SystemActors.CommandProcessor = ActorSystem.ActorOf(Props.Create(() => new AggregateRootCoordinatorActor()), "command-proc");
            
            //SystemActors.SignalRActor = ActorSystem.ActorOf(Props.Create(() => new SignalRActor()), "signalr");
        }
    }
}

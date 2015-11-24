using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using Events;
using Events.Dispatching;
using IoC;
using Mixins;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Demo.Web
{
    public static class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new ContravariantRegistrationSource());

            Types.Referenced.KindOf("Services")
                .Classes()
                .ForAll(t =>
                {
                    builder
                        .RegisterType(t)
                        .AsSelf();
                    t
                        .GetInterfaces()                        
                        .ForAll(i => builder
                            .RegisterType(WrapperFactory.Emit(i))
                            .WithParameter((pi, ctx) => true, (pi, ctx) => ctx.Resolve(t))
                            .AsImplementedInterfaces());
                });

            Types.Referenced.With<MixinAttribute>()
                .ForAll(t => builder
                    .RegisterType(Mixin.Emit(t))
                    .As(t));

            Assemblies.Referenced
                .ForAll(a => builder.RegisterApiControllers(a));

            builder.RegisterType<ServiceProvider>()
                .AsImplementedInterfaces();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            Event.Subscribe(new EventDispatcher(container.Resolve<IServiceProvider>()));
        }
    }

    class ServiceProvider : IServiceProvider
    {
        public ServiceProvider(IComponentContext context)
        {
            Contract.Requires<ArgumentNullException>(context != null);
            Contract.Ensures(Context != null);
            Context = context;
        }

        public IComponentContext Context { get; }

        public object GetService(Type serviceType)
        {
            return Context.Resolve(serviceType);
        }
    }
}

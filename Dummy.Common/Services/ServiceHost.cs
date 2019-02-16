using Dummy.Common.Commands;
using Dummy.Common.Events;
using Dummy.Common.RabbitMq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dummy.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            this._webHost = webHost ?? throw new ArgumentNullException("webHost");
        }

        public void Run() => _webHost.Run();

        public static HostBuilder Create<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;

            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var webHostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<TStartup>();

            return new HostBuilder(webHostBuilder.Build());
        }

#warning Inner classes, remove it from here after.

        public abstract class BuilderBase
        {
            public abstract ServiceHost Build();
        }

        public class HostBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public HostBuilder(IWebHost webHost)
            {
                this._webHost = webHost ?? throw new ArgumentNullException("webHost");
            }

            public BusBuilder UseRabbitMq()
            {
                _bus = (IBusClient)_webHost.Services.GetService(typeof(IBusClient));

                return new BusBuilder(this._webHost, this._bus);
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(this._webHost);
            }
        }

        public class BusBuilder : BuilderBase
        {
            private readonly IWebHost _webHost;
            private IBusClient _bus;

            public BusBuilder(IWebHost webHost, IBusClient bus)
            {
                this._webHost = webHost ?? throw new ArgumentNullException("webHost");
                this._bus = bus ?? throw new ArgumentNullException("bus");
            }

            /// <summary>
            /// Subscribe commands.
            /// </summary>
            /// <typeparam name="TCommand"></typeparam>
            /// <returns></returns>
            public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
            {
                var handler = (ICommandHandler<TCommand>)_webHost.Services
                    .GetService(typeof(ICommandHandler<TCommand>));

                _bus.WithCommandHandlerAsync(handler);

                return this;
            }

            /// <summary>
            /// Subscribe events.
            /// </summary>
            /// <typeparam name="TEvent"></typeparam>
            /// <returns></returns>
            public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
            {
                // Using microsoft extension dependency to resolve the issue to get events services registered.
                using (var serviceScope = _webHost.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    // Gets the event in service collection.
                    var handler = (IEventHandler<TEvent>)serviceScope.ServiceProvider
                        .GetService(typeof(IEventHandler<TEvent>));

                    // Register the service in the bus.
                    _bus.WithEventHandlerAsync(handler);

                    return this;
                }
            }

            public override ServiceHost Build()
            {
                return new ServiceHost(_webHost);
            }
        }
    }
}

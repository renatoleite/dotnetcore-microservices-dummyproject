﻿using System.Reflection;
using System.Threading.Tasks;
using Dummy.Common.Commands;
using Dummy.Common.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration;
using RawRabbit.Instantiation;
using RawRabbit.Pipe.Middleware;

namespace Dummy.Common.RabbitMq
{
    public static class Extensions
    {
        /// <summary>
        /// Commands handler
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="bus"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Task WithCommandHandlerAsync<TCommand>(this IBusClient bus, ICommandHandler<TCommand> handler) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg =>
                cfg.FromQueue(GetQueueName<TCommand>())));

        /// <summary>
        /// Event handler
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="bus"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static Task WithEventHandlerAsync<TEvent>(this IBusClient bus, IEventHandler<TEvent> handler) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(msg => handler.HandleAsync(msg),
                ctx => ctx.UseConsumeConfiguration(cfg =>
                cfg.FromQueue(GetQueueName<TEvent>())));

        /// <summary>
        /// Gets the queue name to avoid issues with multiple services
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static string GetQueueName<T>()
            => $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}";

        /// <summary>
        /// Create the connection with rabbitMQ and adds in the service collection.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var options = new RabbitMqOptions();
            var section = configuration.GetSection("rabbitmq");

            section.Bind(options);

            var client = RawRabbitFactory.CreateSingleton(new RawRabbitOptions
            {
                ClientConfiguration = options
            });

            services.AddSingleton<IBusClient>(_ => client);
        }
    }

    public class RabbitMqOptions : RawRabbitConfiguration
    {
    }
}

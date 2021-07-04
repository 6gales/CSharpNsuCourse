using System;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.MessageContract
{
    public static class MassTransitServiceCollectionExtensions
    {
        private const string RabbitMqSection = "MassTransit";

        public static IServiceCollection AddMassTransitConfiguration<T>(this IServiceCollection services, IConfiguration configuration)
            where T : class, IConsumer
        {
            var transitConfiguration = new MassTransitConfiguration();
            configuration.GetSection(RabbitMqSection).Bind(transitConfiguration);
            services.AddSingleton<IMassTransitConfiguration>(transitConfiguration);

            services.AddMassTransit(serviceProvider =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(config =>
                    {
                        var host = config.Host(
                            new Uri(transitConfiguration.RabbitMqAddress),
                            h =>
                            {
                                h.Username(transitConfiguration.UserName);
                                h.Password(transitConfiguration.Password);
                            });

                        config.Durable = transitConfiguration.Durable;
                        config.PurgeOnStartup = transitConfiguration.PurgeOnStartup;

                        config.ReceiveEndpoint(host,
                            transitConfiguration.InputQueue, ep =>
                            {
                                ep.PrefetchCount = 1;
                                ep.ConfigureConsumer<T>(serviceProvider);
                            });
                    });
                },
                collectionConfigurator => { collectionConfigurator.AddConsumers(typeof(T).Assembly); });

            return services;
        }
    }
}
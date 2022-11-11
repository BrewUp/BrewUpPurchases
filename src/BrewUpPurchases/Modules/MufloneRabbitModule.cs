using BrewUpPurchases.Consumers.RMQ.Consumers;
using Microsoft.Extensions.Logging.Abstractions;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Factories;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUpPurchases.Modules;

public sealed class MufloneRabbitModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 99;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
        var repository = serviceProvider.GetService<IRepository>();

        var rabbitMQConfiguration = new RabbitMQConfiguration(
            builder.Configuration["BrewUp:RabbitMQSettings:BrokerUrl"],
            builder.Configuration["BrewUp:RabbitMQSettings:Login"],
            builder.Configuration["BrewUp:RabbitMQSettings:Password"], builder.Configuration["BrewUp:ClientId"]);
        var rabbitMQReference =
            new RabbitMQReference(builder.Configuration["BrewUp:RabbitMQSettings:ExchangeCommandName"],
                builder.Configuration["BrewUp:RabbitMQSettings:QueueCommandName"],
                builder.Configuration["BrewUp:RabbitMQSettings:ExchangeEventsName"],
                builder.Configuration["BrewUp:RabbitMQSettings:QueueEventsName"]);
        var mufloneConnectionFactory = new MufloneConnectionFactory(rabbitMQConfiguration, new NullLoggerFactory());

        var consumers = new List<IConsumer>
        {
            new CreaOrdineFornitoreConsumer(repository!, rabbitMQReference, mufloneConnectionFactory, loggerFactory!)
            //new CreateOrderConsumer(rabbitMQReference, mufloneConnectionFactory, new NullLoggerFactory()),
            //new OrderCreatedConsumer(rabbitMQReference, mufloneConnectionFactory, new NullLoggerFactory())
        };

        builder.Services.AddMufloneTransportRabbitMQ(rabbitMQConfiguration, rabbitMQReference, consumers);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}
using BrewUpPurchases.Domain.Consumers;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using BrewUpPurchases.Modules.Purchases.Consumers;
using BrewUpPurchases.ReadModel.MongoDb;
using BrewUpPurchases.Shared;
using BrewUpPurchases.Shared.Configuration;
using Muflone.Factories;
using Muflone.Persistence;
using Muflone.Transport.Azure;
using Muflone.Transport.Azure.Abstracts;
using Muflone.Transport.Azure.Models;

namespace BrewUpPurchases.Modules;

public sealed class InfrastructureModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 98;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddEventStore(builder.Configuration.GetSection("BrewUp:EventStoreSettings").Get<EventStoreSettings>());

        var serviceProvider = builder.Services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetService<ILoggerFactory>();

        var domainEventHandlerFactoryAsync = serviceProvider.GetService<IDomainEventHandlerFactoryAsync>();
        var repository = serviceProvider.GetService<IRepository>();

        var clientId = builder.Configuration["BrewUp:ClientId"];
        var serviceBusConnectionString = builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"];
        var azureBusConfiguration =
            new AzureServiceBusConfiguration(serviceBusConnectionString, nameof(CreaOrdineFornitore), clientId);

        var consumers = new List<IConsumer>
        {
            new CreaOrdineFornitoreConsumer(repository!, azureBusConfiguration with { TopicName = nameof(CreaOrdineFornitore) }, loggerFactory!),
            new OrdineFornitoreInseritoConsumer(domainEventHandlerFactoryAsync!, azureBusConfiguration with { TopicName = nameof(OrdineFornitoreInserito) }, loggerFactory!),

            new EvadiOrdineFornitoreConsumer(repository!, azureBusConfiguration with { TopicName = nameof(EvadiOrdineFornitore)}, loggerFactory!),
            new OrdineFornitoreEvasoConsumer(domainEventHandlerFactoryAsync!, azureBusConfiguration with { TopicName = nameof(OrdineFornitoreEvaso) }, loggerFactory!)
        };
        builder.Services.AddMufloneTransportAzure(
            new AzureServiceBusConfiguration(builder.Configuration["BrewUp:ServiceBusSettings:ConnectionString"], "",
                clientId), consumers);

        var mongoDbSettings = new MongoDbSettings();
        builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
        builder.Services.AddEventstoreMongoDb(mongoDbSettings);

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints) => endpoints;
}
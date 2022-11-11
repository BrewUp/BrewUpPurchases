using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using Microsoft.Extensions.Logging;
using Muflone.Factories;
using Muflone.Messages.Events;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUpPurchases.Modules.Purchases.Consumers;

public sealed class OrdineFornitoreEvasoConsumer : DomainEventConsumerBase<OrdineFornitoreEvaso>
{
    protected override IEnumerable<IDomainEventHandlerAsync<OrdineFornitoreEvaso>> HandlersAsync { get; }

    public OrdineFornitoreEvasoConsumer(IDomainEventHandlerFactoryAsync domainEventHandlerFactoryAsync,
        AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
        ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        HandlersAsync = domainEventHandlerFactoryAsync.CreateDomainEventHandlersAsync<OrdineFornitoreEvaso>();
    }
}
using BrewUpPurchases.Domain.CommandHandlers;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUpPurchases.Consumers.Azure.Consumers;

public sealed class CreaOrdineFornitoreConsumer : CommandConsumerBase<CreaOrdineFornitore>
{
    protected override ICommandHandlerAsync<CreaOrdineFornitore> HandlerAsync { get; }

    public CreaOrdineFornitoreConsumer(IRepository repository,
        AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
        ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        HandlerAsync = new CreaOrdineFornitoreCommandHandler(repository, loggerFactory);
    }
}
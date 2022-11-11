using BrewUpPurchases.Domain.CommandHandlers;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.Azure.Consumers;
using Muflone.Transport.Azure.Models;

namespace BrewUpPurchases.Domain.Consumers;

public sealed class EvadiOrdineFornitoreConsumer : CommandConsumerBase<EvadiOrdineFornitore>
{
    protected override ICommandHandlerAsync<EvadiOrdineFornitore> HandlerAsync { get; }

    public EvadiOrdineFornitoreConsumer(IRepository repository,
        AzureServiceBusConfiguration azureServiceBusConfiguration, ILoggerFactory loggerFactory,
        ISerializer? messageSerializer = null) : base(azureServiceBusConfiguration, loggerFactory, messageSerializer)
    {
        HandlerAsync = new EvadiOrdineFornitoreCommandHandler(repository ,loggerFactory);
    }
}
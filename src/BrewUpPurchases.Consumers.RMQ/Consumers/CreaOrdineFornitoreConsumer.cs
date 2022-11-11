using BrewUpPurchases.Domain.CommandHandlers;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Messages.Commands;
using Muflone.Persistence;
using Muflone.Transport.RabbitMQ.Abstracts;
using Muflone.Transport.RabbitMQ.Consumers;
using Muflone.Transport.RabbitMQ.Models;

namespace BrewUpPurchases.Consumers.RMQ.Consumers;

public class CreaOrdineFornitoreConsumer : CommandConsumerBase<CreaOrdineFornitore>
{
    protected override ICommandHandlerAsync<CreaOrdineFornitore> HandlerAsync { get; }

    public CreaOrdineFornitoreConsumer(IRepository repository, RabbitMQReference rabbitMQReference,
        IMufloneConnectionFactory mufloneConnectionFactory, ILoggerFactory loggerFactory) : base(rabbitMQReference,
        mufloneConnectionFactory, loggerFactory)
    {
        HandlerAsync = new CreaOrdineFornitoreCommandHandler(repository, loggerFactory);
    }
}
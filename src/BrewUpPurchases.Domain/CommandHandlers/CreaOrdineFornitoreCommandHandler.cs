using BrewUpPurchases.Domain.Abstracts;
using BrewUpPurchases.Domain.Entities;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpPurchases.Domain.CommandHandlers;

public sealed class CreaOrdineFornitoreCommandHandler : CommandHandlerAsync<CreaOrdineFornitore>
{
    public CreaOrdineFornitoreCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(CreaOrdineFornitore command, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var ordineFornitore = OrdineFornitore.CreaOrdineFornitore(command.OrderId, command.OrderNumber,
                command.Fornitore, command.DataInserimento, command.DataPrevistaConsegna, command.Rows,
                command.MessageId);

            await Repository.SaveAsync(ordineFornitore, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
using BrewUpPurchases.Domain.Abstracts;
using BrewUpPurchases.Domain.Entities;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpPurchases.Domain.CommandHandlers;

public class EvadiOrdineFornitoreCommandHandler : CommandHandlerAsync<EvadiOrdineFornitore>
{
    public EvadiOrdineFornitoreCommandHandler(IRepository repository, ILoggerFactory loggerFactory) : base(repository, loggerFactory)
    {
    }

    public override async Task HandleAsync(EvadiOrdineFornitore command, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var ordineFornitore = await Repository.GetByIdAsync<OrdineFornitore>(command.OrderId.Value);
            ordineFornitore.EvadiOrdineFornitore(command.Rows, command.DataEffettivaConsegna);

            await Repository.SaveAsync(ordineFornitore, Guid.NewGuid());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}
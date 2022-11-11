using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.EventsHandlers;

public sealed class OrdineFornitoreEvasoEventHandler : StoreDomainEventHandler<OrdineFornitoreEvaso>
{
    private readonly IPurchaseService _purchaseService;

    public OrdineFornitoreEvasoEventHandler(ILoggerFactory loggerFactory,
        IPurchaseService purchaseService) : base(loggerFactory)
    {
        _purchaseService = purchaseService;
    }

    public override async Task HandleAsync(OrdineFornitoreEvaso @event, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _purchaseService.EvadiOrdinFornitoreAsync(@event.OrderId, @event.DataEffettivaConsegna, @event.Rows);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}
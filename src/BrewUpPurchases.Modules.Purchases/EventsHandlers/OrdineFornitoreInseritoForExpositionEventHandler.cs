using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.EventsHandlers;

public sealed class OrdineFornitoreInseritoForExpositionEventHandler : StoreDomainEventHandler<OrdineFornitoreInserito>
{
    private readonly ISupplierExpositionService _supplierExpositionService;

    public OrdineFornitoreInseritoForExpositionEventHandler(ILoggerFactory loggerFactory,
        ISupplierExpositionService supplierExpositionService) : base(loggerFactory)
    {
        _supplierExpositionService = supplierExpositionService;
    }

    public override async Task HandleAsync(OrdineFornitoreInserito @event, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _supplierExpositionService.UpdateSupplierExpositionAsync(@event.Fornitore.Id,
                @event.Fornitore.Denominazione, new Import(@event.Rows.Sum(row => row.Quantity.Value * 2)));
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}
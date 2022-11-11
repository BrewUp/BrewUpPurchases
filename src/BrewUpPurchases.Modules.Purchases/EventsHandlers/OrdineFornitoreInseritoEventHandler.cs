﻿using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.EventsHandlers;

public sealed class OrdineFornitoreInseritoEventHandler : StoreDomainEventHandler<OrdineFornitoreInserito>
{
    private readonly IPurchaseService _purchaseService;

    public OrdineFornitoreInseritoEventHandler(ILoggerFactory loggerFactory,
        IPurchaseService purchaseService) : base(loggerFactory)
    {
        _purchaseService = purchaseService;
    }

    public override async Task HandleAsync(OrdineFornitoreInserito @event, CancellationToken cancellationToken = new ())
    {
        if (cancellationToken.IsCancellationRequested)
            cancellationToken.ThrowIfCancellationRequested();

        try
        {
            await _purchaseService.CreateSupplierOrderAsync(@event.OrderId, @event.OrderNumber, @event.Fornitore,
                @event.DataInserimento, @event.DataPrevistaConsegna, @event.Rows);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}
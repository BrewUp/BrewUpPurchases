using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using Microsoft.Extensions.Logging;
using Muflone.Persistence;

namespace BrewUpPurchases.Modules.Purchases.Concretes;

public sealed class StoreOrchestrator : StoreBaseOrchestrator, IStoreOrchestrator
{
    private readonly IServiceBus _serviceBus;

    public StoreOrchestrator(ILoggerFactory loggerFactory,
        IServiceBus serviceBus) : base(loggerFactory)
    {
        _serviceBus = serviceBus;
    }

    public async Task<string> CreaOrdineFornitoreAsync(SupplierOrderJson orderToCreate)
    {
        if (string.IsNullOrEmpty(orderToCreate.OrderId))
            orderToCreate.OrderId = Guid.NewGuid().ToString();

        var creaOrdineFornitore = new CreaOrdineFornitore(new OrderId(new Guid(orderToCreate.OrderId)), Guid.NewGuid(),
            new OrderNumber(orderToCreate.OrderNumber),
            new Fornitore(new FornitoreId(orderToCreate.Fornitore.FornitoreId),
                new DenominazioneFornitore(orderToCreate.Fornitore.Denominazione)),
            new DataInserimento(orderToCreate.DataInserimento),
            new DataPrevistaConsegna(orderToCreate.DataPrevistaConsegna),
            orderToCreate.Rows.Select(r => new OrderRow(new RowId(r.RowId), 
                new Ingredient(new IngredientId(r.IngredientId), new IngredientName(r.IngredientName)),
                new Quantity(r.Quantity))));

        await _serviceBus.SendAsync(creaOrdineFornitore);

        return orderToCreate.OrderId;
    }

    public async Task EvadiOrdineFornitoreAsync(SupplierOrderJson orderToShip)
    {
        // Controllare nel ReadModel che l'Ordine esista!

        var evadiOrdine = new EvadiOrdineFornitore(new OrderId(new Guid(orderToShip.OrderId)),
            new DataEffettivaConsegna(orderToShip.DataPrevistaConsegna),
            orderToShip.Rows.Select(r => new OrderRow(new RowId(r.RowId),
                new Ingredient(new IngredientId(r.IngredientId), new IngredientName(r.IngredientName)),
                new Quantity(r.Quantity))));

        await _serviceBus.SendAsync(evadiOrdine);
    }
}
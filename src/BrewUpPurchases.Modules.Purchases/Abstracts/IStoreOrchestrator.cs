using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public interface IStoreOrchestrator
{
    Task<string> CreaOrdineFornitoreAsync(SupplierOrderJson orderToCreate);
    Task EvadiOrdineFornitoreAsync(SupplierOrderJson orderToShip);
}
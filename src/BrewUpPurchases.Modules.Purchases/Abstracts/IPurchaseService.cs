using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public interface IPurchaseService
{
    Task CreateSupplierOrderAsync(OrderId orderId, OrderNumber orderNumber, Fornitore fornitore,
        DataInserimento dataInserimento, DataPrevistaConsegna dataPrevistaConsegna, IEnumerable<OrderRow> rows);

    Task EvadiOrdinFornitoreAsync(OrderId orderId, DataEffettivaConsegna dataEffettivaConsegna,
        IEnumerable<OrderRow> rows);

    Task<IEnumerable<SupplierOrderJson>> GetSupplierOrdersAsync();
}
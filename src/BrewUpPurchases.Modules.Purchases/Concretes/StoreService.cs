using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.ReadModel.Abstracts;
using BrewUpPurchases.ReadModel.Models;
using BrewUpPurchases.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.Concretes;

public sealed class StoreService : StoreBaseService, IStoreService
{
    public StoreService(IPersister persister, ILoggerFactory loggerFactory)
        : base(persister, loggerFactory)
    {
    }

    public async Task CreateSupplierOrderAsync(OrderId orderId, OrderNumber orderNumber, Fornitore fornitore,
        DataInserimento dataInserimento, DataPrevistaConsegna dataPrevistaConsegna, IEnumerable<OrderRow> rows)
    {
        try
        {
            var supplierOrder = SupplierOrder.CreateSupplierOrder(orderId, orderNumber, fornitore, dataInserimento,
                dataPrevistaConsegna, rows);

            await Persister.InsertAsync(supplierOrder);
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<SupplierOrderJson>> GetSupplierOrdersAsync()
    {
        try
        {
            var orders = await Persister.FindAsync<SupplierOrder>();
            var ordersArray = orders as SupplierOrder[] ?? orders.ToArray();

            return ordersArray.Any()
                ? ordersArray.Select(o => o.ToJson())
                : Enumerable.Empty<SupplierOrderJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}
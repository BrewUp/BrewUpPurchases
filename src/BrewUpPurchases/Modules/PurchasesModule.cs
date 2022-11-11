using BrewUpPurchases.Modules.Purchases;
using BrewUpPurchases.Modules.Purchases.Endpoints;

namespace BrewUpPurchases.Modules;

public sealed class PurchasesModule : IModule
{
    public bool IsEnabled => true;
    public int Order => 0;

    public IServiceCollection RegisterModule(WebApplicationBuilder builder)
    {
        builder.Services.AddPurchaseModule();

        return builder.Services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        const string storeTag = "Purchases";

        endpoints.MapPost("v1/purchases/orders", PurchasesEndpoints.HandleCreaOrdineFornitore)
            .WithName("CreaOrdineFornitore")
            .WithTags(storeTag);

        endpoints.MapPut("v1/purchases/orders/{orderId}", PurchasesEndpoints.HandleEvadiOrdineFornitore)
            .WithName("EvadiOrdineFOrnitore")
            .WithTags(storeTag);

        endpoints.MapGet("v1/purchases/orders", PurchasesEndpoints.HandleGetSupplierOrders)
            .WithName("GetSupplierOrders")
            .WithTags(storeTag);

        return endpoints;
    }
}
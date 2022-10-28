using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public abstract class StoreBaseOrchestrator
{
    protected ILogger Logger;

    protected StoreBaseOrchestrator(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }
}
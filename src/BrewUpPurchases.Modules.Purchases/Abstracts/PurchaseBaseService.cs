using BrewUpPurchases.ReadModel.Abstracts;
using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public abstract class PurchaseBaseService
{
    protected readonly IPersister Persister;
    protected readonly ILogger Logger;

    protected PurchaseBaseService(IPersister persister,
        ILoggerFactory loggerFactory)
    {
        Persister = persister;
        Logger = loggerFactory.CreateLogger(GetType());
    }
}
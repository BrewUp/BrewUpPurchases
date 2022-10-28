using Microsoft.Extensions.Logging;
using Muflone.Messages.Events;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public abstract class StoreDomainEventHandler<T> : IDomainEventHandlerAsync<T> where T : class, IDomainEvent
{
    protected ILogger Logger;

    protected StoreDomainEventHandler(ILoggerFactory loggerFactory)
    {
        Logger = loggerFactory.CreateLogger(GetType());
    }

    public abstract Task HandleAsync(T @event, CancellationToken cancellationToken = new());

    #region Dispose
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~StoreDomainEventHandler()
    {
        Dispose(false);
    }
    #endregion
}
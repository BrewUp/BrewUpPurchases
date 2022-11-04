using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;

public sealed class OrdineFornitoreEvaso : DomainEvent
{
    public readonly OrderId OrderId;
    public readonly DataEffettivaConsegna DataEffettivaConsegna;

    public readonly IEnumerable<OrderRow> Rows;

    public OrdineFornitoreEvaso(OrderId aggregateId, DataEffettivaConsegna dataEffettivaConsegna,
        IEnumerable<OrderRow> rows) : base(aggregateId)
    {
        OrderId = aggregateId;

        DataEffettivaConsegna = dataEffettivaConsegna;
        Rows = rows;
    }
}
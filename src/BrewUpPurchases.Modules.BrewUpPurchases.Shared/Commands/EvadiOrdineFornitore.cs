using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;

public sealed class EvadiOrdineFornitore : Command
{
    public readonly OrderId OrderId;
    public readonly DataEffettivaConsegna DataEffettivaConsegna;

    public readonly IEnumerable<OrderRow> Rows;

    public EvadiOrdineFornitore(OrderId aggregateId, DataEffettivaConsegna dataEffettivaConsegna,
        IEnumerable<OrderRow> rows) : base(aggregateId)
    {
        OrderId = aggregateId;

        DataEffettivaConsegna = dataEffettivaConsegna;
        Rows = rows;
    }
}
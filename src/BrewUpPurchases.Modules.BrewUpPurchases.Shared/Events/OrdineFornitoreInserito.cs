using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using Muflone.Messages.Events;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;

public sealed class OrdineFornitoreInserito : DomainEvent
{
    public readonly OrderId OrderId;
    public readonly OrderNumber OrderNumber;

    public readonly Fornitore Fornitore;

    public readonly DataInserimento DataInserimento;
    public readonly DataPrevistaConsegna DataPrevistaConsegna;

    public readonly IEnumerable<OrderRow> Rows;

    public OrdineFornitoreInserito(OrderId aggregateId, Guid commitId, OrderNumber orderNumber, Fornitore fornitore,
        DataInserimento dataInserimento, DataPrevistaConsegna dataPrevistaConsegna, IEnumerable<OrderRow> rows)
        : base(aggregateId, commitId)
    {
        OrderId = aggregateId;
        OrderNumber = orderNumber;

        Fornitore = fornitore;

        DataInserimento = dataInserimento;
        DataPrevistaConsegna = dataPrevistaConsegna;

        Rows = rows;
    }
}
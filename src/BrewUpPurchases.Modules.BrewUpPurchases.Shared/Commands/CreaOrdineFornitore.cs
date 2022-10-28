using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using Muflone.Messages.Commands;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Commands;

public sealed class CreaOrdineFornitore : Command
{
    public readonly OrderId OrderId;
    public readonly OrderNumber OrderNumber;

    public readonly Fornitore Fornitore;

    public readonly DataInserimento DataInserimento;
    public readonly DataPrevistaConsegna DataPrevistaConsegna;

    public readonly IEnumerable<OrderRow> Rows;

    public CreaOrdineFornitore(OrderId aggregateId, Guid commitId, OrderNumber orderNumber, Fornitore fornitore,
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
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Events;
using Muflone.Core;

namespace BrewUpPurchases.Domain.Entities;

public sealed class OrdineFornitore : AggregateRoot
{
    private OrderNumber _orderNumber = default!;

    private Fornitore _fornitore = default!;

    private DataInserimento _dataInserimento = default!;
    private DataPrevistaConsegna _dataPrevistaConsegna = default!;
    private DataEffettivaConsegna _dataEffettivaConsegna = default!;

    private IEnumerable<OrderRow> _rows = default!;

    protected OrdineFornitore()
    {}

    internal static OrdineFornitore CreaOrdineFornitore(OrderId orderId, OrderNumber orderNumber, Fornitore fornitore,
        DataInserimento dataInserimento, DataPrevistaConsegna dataPrevistaConsegna, IEnumerable<OrderRow> rows,
        Guid correlationId)
    {
        return new OrdineFornitore(orderId, orderNumber, fornitore, dataInserimento, dataPrevistaConsegna,
            rows, correlationId);
    }

    private OrdineFornitore(OrderId orderId, OrderNumber orderNumber, Fornitore fornitore,
        DataInserimento dataInserimento, DataPrevistaConsegna dataPrevistaConsegna, IEnumerable<OrderRow> rows,
        Guid correlationId)
    {
        RaiseEvent(new OrdineFornitoreInserito(orderId, correlationId, orderNumber, fornitore, dataInserimento,
            dataPrevistaConsegna, rows));
    }

    private void Apply(OrdineFornitoreInserito @event)
    {
        Id = @event.OrderId;

        _orderNumber = @event.OrderNumber;

        _fornitore = @event.Fornitore;

        _dataInserimento = @event.DataInserimento;
        _dataPrevistaConsegna = @event.DataPrevistaConsegna;

        _rows = @event.Rows;
    }

    internal void EvadiOrdineFornitore(IEnumerable<OrderRow> rows, DataEffettivaConsegna dataEffettivaConsegna)
    {
        var newRows =
            (from row in rows
                let chkRow = _rows.FirstOrDefault(r => r.Ingredient.IngredientId.Equals(row.Ingredient.IngredientId))
                where chkRow != null
                select row).ToList();
        var eventRows = Enumerable.Empty<OrderRow>();
        eventRows = eventRows.Concat(newRows);

        RaiseEvent(new OrdineFornitoreEvaso(new OrderId(Id.Value), dataEffettivaConsegna, eventRows));
    }

    private void Apply(OrdineFornitoreEvaso @event)
    {
        _dataEffettivaConsegna = @event.DataEffettivaConsegna;
        _rows = @event.Rows;
    }
}
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using BrewUpPurchases.ReadModel.Abstracts;

namespace BrewUpPurchases.ReadModel.Models;

public class SupplierOrder : ModelBase
{
    public string OrderNumber { get; private set; } = string.Empty;

    public FornitoreJson Fornitore { get; private set; } = new();

    public DateTime DataInserimento { get; private set; } = DateTime.MinValue;
    public DateTime DataPrevistaConsegna { get; private set; } = DateTime.MinValue;
    public DateTime DataEffettivaConsegna { get; private set; } = DateTime.MinValue;

    public IEnumerable<SupplierOrderRowsJson> Rows { get; private set; } = Enumerable.Empty<SupplierOrderRowsJson>();

    protected SupplierOrder()
    {}

    public static SupplierOrder CreateSupplierOrder(OrderId orderId, OrderNumber orderNumber, Fornitore fornitore,
        DataInserimento dataInserimento, DataPrevistaConsegna dataPrevistaConsegna, IEnumerable<OrderRow> rows) => new(orderId.ToString(),
        orderNumber.Value,
        new FornitoreJson
        {
            FornitoreId = fornitore.Id.Value, Denominazione = fornitore.Denominazione.Value
        },
        dataInserimento.Value, dataPrevistaConsegna.Value,
        rows.Select(r => new SupplierOrderRowsJson
        {
            RowId = r.RowId.Value,

            IngredientId = r.Ingredient.IngredientId.Value,
            IngredientName = r.Ingredient.IngredientName.Value,

            Quantity = r.Quantity.Value
        }));

    private SupplierOrder(string orderId, string orderNumer, FornitoreJson fornitore, DateTime dataInserimento,
        DateTime dataPrevistaConsegna, IEnumerable<SupplierOrderRowsJson> rows)
    {
        Id = orderId;
        OrderNumber = orderNumer;

        Fornitore = fornitore;

        DataInserimento = dataInserimento;
        DataPrevistaConsegna = dataPrevistaConsegna;

        Rows = rows;
    }

    public void EvadiOrdineFornitore(DataEffettivaConsegna dataEffettivaConsegna, IEnumerable<OrderRow> rows)
    {
        DataEffettivaConsegna = dataEffettivaConsegna.Value;

        Rows = rows.Select(r => new SupplierOrderRowsJson
        {
            RowId = r.RowId.Value,

            IngredientId = r.Ingredient.IngredientId.Value,
            IngredientName = r.Ingredient.IngredientName.Value,

            Quantity = r.Quantity.Value
        });
    }

    public SupplierOrderJson ToJson() => new()
    {
        OrderId = Id,
        OrderNumber = OrderNumber,

        Fornitore = Fornitore,

        DataInserimento = DataInserimento,
        DataPrevistaConsegna = DataPrevistaConsegna,

        Rows = Rows
    };
}
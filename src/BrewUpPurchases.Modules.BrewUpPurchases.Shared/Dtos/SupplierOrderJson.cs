namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;

public class SupplierOrderJson
{
    public string OrderId { get; set; } = string.Empty;
    public string OrderNumber { get; set; } = string.Empty;

    public FornitoreJson Fornitore { get; set; } = new();

    public DateTime DataInserimento { get; set; } = DateTime.UtcNow;
    public DateTime DataPrevistaConsegna { get; set; } = DateTime.MinValue;

    public IEnumerable<SupplierOrderRowsJson> Rows { get; set; } = Enumerable.Empty<SupplierOrderRowsJson>();
}
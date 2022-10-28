namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;

public class SupplierOrderRowsJson
{
    public string RowId { get; set; } = string.Empty;

    public string IngredientId { get; set; } = string.Empty;
    public string IngredientName { get; set; } = string.Empty;

    public double Quantity { get; set; } = 0;
    public double UnitCost { get; set; } = 0;
}
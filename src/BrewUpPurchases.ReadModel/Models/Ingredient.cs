using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using BrewUpPurchases.ReadModel.Abstracts;

namespace BrewUpPurchases.ReadModel.Models;

public class Ingredient : ModelBase
{
    public string Name { get; private set; } = string.Empty;

    protected Ingredient()
    { }

    public static Ingredient CreateIngredient(IngredientId ingredientId, IngredientName name) =>
        new(ingredientId.Value, name.Value);

    private Ingredient(string ingredientId, string ingredientName)
    {
        Id = ingredientId;
        Name = ingredientName;
    }

    public IngredientJson ToJson() => new()
    {
        Id = Id,
        Name = Name
    };
}
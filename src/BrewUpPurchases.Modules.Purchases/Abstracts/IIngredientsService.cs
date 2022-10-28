using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public interface IIngredientsService
{
    Task<string> AddIngredientAsync(IngredientJson ingredientToCreate);
    Task<IEnumerable<IngredientJson>> GetIngredientsAsync();
}
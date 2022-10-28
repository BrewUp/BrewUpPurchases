using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.ReadModel.Abstracts;
using BrewUpPurchases.Shared.Concretes;
using Microsoft.Extensions.Logging;
using Ingredient = BrewUpPurchases.ReadModel.Models.Ingredient;

namespace BrewUpPurchases.Modules.Purchases.Concretes;

public sealed class IngredientsService : StoreBaseService, IIngredientsService
{
    public IngredientsService(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
    }

    public async Task<string> AddIngredientAsync(IngredientJson ingredientToCreate)
    {
        try
        {
            if (string.IsNullOrEmpty(ingredientToCreate.Id))
                ingredientToCreate.Id = Guid.NewGuid().ToString();

            var ingredient = ReadModel.Models.Ingredient.CreateIngredient(new IngredientId(ingredientToCreate.Id),
                new IngredientName(ingredientToCreate.Name));
            await Persister.InsertAsync(ingredient);

            return ingredient.Id;
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }

    public async Task<IEnumerable<IngredientJson>> GetIngredientsAsync()
    {
        try
        {
            var ingredients = await Persister.FindAsync<ReadModel.Models.Ingredient>();
            var ingredientsArray = ingredients as Ingredient[] ?? ingredients.ToArray();

            return ingredientsArray.Any()
                ? ingredientsArray.Select(i => i.ToJson())
                : Enumerable.Empty<IngredientJson>();
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}
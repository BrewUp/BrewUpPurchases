using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Validators;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace BrewUpPurchases.Modules.Purchases.Endpoints;

public static class PurchasesEndpoints
{
    #region Ingredients
    public static async Task<IResult> HandleCreateIngredient(IIngredientsService ingredientsService,
        IValidator<IngredientJson> validator,
        ValidationHandler validationHandler,
        IngredientJson body)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        var ingredientId = await ingredientsService.AddIngredientAsync(body);

        return Results.Created(new Uri($"v1/purchases/ingredients/{ingredientId}"), ingredientId);
    }

    public static async Task<IResult> HandleGetIngredientsAsync(IIngredientsService ingredientsService)
    {
        var ingredients = await ingredientsService.GetIngredientsAsync();

        return Results.Ok(ingredients);
    }
    #endregion

    #region SupplierOrder
    public static async Task<IResult> HandleCreaOrdineFornitore(IStoreOrchestrator storeOrchestrator,
        IValidator<SupplierOrderJson> validator,
        ValidationHandler validationHandler,
        SupplierOrderJson body)
    {
        await validationHandler.ValidateAsync(validator, body);
        if (!validationHandler.IsValid)
            return Results.BadRequest(validationHandler.Errors);

        var orderId = await storeOrchestrator.CreaOrdineFornitoreAsync(body);

        return Results.Accepted($"v1/purchases/orders/{orderId}");
    }

    public static async Task<IResult> HandleGetSupplierOrders(IStoreService storeService)
    {
        var orders = await storeService.GetSupplierOrdersAsync();

        return Results.Ok(orders);
    }
    #endregion
}
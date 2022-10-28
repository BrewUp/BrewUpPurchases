using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using FluentValidation;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Validators;

public class IngredientValidator : AbstractValidator<IngredientJson>
{
    public IngredientValidator()
    {
        RuleFor(v => v.Name).NotEmpty();
    }
}
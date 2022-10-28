using BrewUpPurchases.Modules.BrewUpPurchases.Shared.Dtos;
using FluentValidation;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.Validators;

public class OrdineFornitoreValidator : AbstractValidator<SupplierOrderJson>
{
    public OrdineFornitoreValidator()
    {
        RuleFor(v => v.OrderNumber).NotEmpty();

        RuleFor(h => h.Fornitore.FornitoreId).NotEmpty();
        RuleFor(h => h.Fornitore.Denominazione).NotEmpty();

        RuleFor(h => h.DataInserimento).GreaterThan(DateTime.MinValue);
    }
}
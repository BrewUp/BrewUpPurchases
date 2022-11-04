using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;

namespace BrewUpPurchases.Modules.Purchases.Abstracts;

public interface ISupplierExpositionService
{
    Task UpdateSupplierExpositionAsync(FornitoreId fornitoreId, DenominazioneFornitore denominazione, Import import);
}
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.Modules.Purchases.Abstracts;
using BrewUpPurchases.ReadModel.Abstracts;
using BrewUpPurchases.ReadModel.Models;
using BrewUpPurchases.Shared.Concretes;
using Microsoft.Extensions.Logging;

namespace BrewUpPurchases.Modules.Purchases.Concretes;

public sealed class SupplierExpositionService : StoreBaseService, ISupplierExpositionService
{
    public SupplierExpositionService(IPersister persister, ILoggerFactory loggerFactory) : base(persister, loggerFactory)
    {
    }

    public async Task UpdateSupplierExpositionAsync(FornitoreId fornitoreId, DenominazioneFornitore denominazione, Import import)
    {
        try
        {
            var exposition = await Persister.GetByIdAsync<SupplierExposition>(fornitoreId.Value);
            if (string.IsNullOrEmpty(exposition.Id))
            {
                exposition = SupplierExposition.CreateSupplierExposition(fornitoreId, denominazione, import);
                await Persister.InsertAsync(exposition);
            }
            else
            {
                exposition.UpdateImport(import);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
            throw;
        }
    }
}
using BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;
using BrewUpPurchases.ReadModel.Abstracts;

namespace BrewUpPurchases.ReadModel.Models;

public class SupplierExposition : ModelBase
{
    public string Denominazione { get; private set; }
    public double Import { get; private set; }

    protected SupplierExposition()
    {}

    public static SupplierExposition CreateSupplierExposition(FornitoreId fornitoreId,
        DenominazioneFornitore denominazione, Import import) => new(fornitoreId.Value, denominazione.Value, import.Value);

    private SupplierExposition(string fornitoreId, string denominazione, double import)
    {
        Id = fornitoreId;

        Denominazione = denominazione;
        Import = import;
    }

    public void UpdateImport(Import import) => Import = import.Value;
}
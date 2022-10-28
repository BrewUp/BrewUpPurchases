using Muflone.Core;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;

public class BeerId : DomainId
{
    public BeerId(Guid value) : base(value)
    {
    }
}
using Muflone.Core;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;

public sealed class BatchId : DomainId
{
    public BatchId(Guid value) : base(value)
    {
    }
}
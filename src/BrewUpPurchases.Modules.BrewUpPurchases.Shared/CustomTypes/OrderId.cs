using Muflone.Core;

namespace BrewUpPurchases.Modules.BrewUpPurchases.Shared.CustomTypes;

public sealed class OrderId : DomainId
{
    public OrderId(Guid value) : base(value)
    {
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}
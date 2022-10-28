namespace BrewUpPurchases.ReadModel.Abstracts;

public interface IModelBase
{
    string Id { get; }
    bool IsDeleted { get; }
}
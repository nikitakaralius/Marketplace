using Marketplace.Domain.ValueObjects;

namespace Marketplace.Domain.Entities;

public sealed class ClassifiedAd
{
    private readonly UserId _ownerId;
    private string _title;
    private string _description;
    private decimal _price;

    public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
    {
        Id = id;
        _ownerId = ownerId;
    }

    public ClassifiedAdId Id { get; }

    public void SetTitle(string title) => _title = title;

    public void UpdateDescription(string description) => _description = description;

    public void UpdatePrice(decimal price) => _price = price;
}

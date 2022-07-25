using Marketplace.Domain.Exceptions;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework;

namespace Marketplace.Domain.Entities;

public sealed class ClassifiedAd : Entity<ClassifiedAd>
{
    public enum AdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }

    public ClassifiedAd(ClassifiedAdId id, UserId ownerId)
    {
        Id = id;
        OwnerId = ownerId;
        State = AdState.Inactive;
        EnsureValidState();
    }

    public ClassifiedAdId Id { get; }

    public UserId OwnerId { get; }

    public ClassifiedAdTitle? Title { get; private set; }

    public ClassifiedAdDescription? Description { get; private set; }

    public Price? Price { get; private set; }

    public UserId? ApprovedBy { get; private set; }

    public AdState State { get; private set; }

    public void SetTitle(ClassifiedAdTitle title)
    {
        Title = title;
        EnsureValidState();
    }

    public void UpdateDescription(ClassifiedAdDescription description)
    {
        Description = description;
        EnsureValidState();
    }

    public void UpdatePrice(Price price)
    {
        Price = price;
        EnsureValidState();
    }

    public void RequestToPublish()
    {
        State = AdState.PendingReview;
        EnsureValidState();
    }

    private void EnsureValidState()
    {
        bool valid = State switch
        {
            AdState.PendingReview =>
                Title is not null
                && Description is not null
                && Price?.Amount > 0,
            AdState.Active =>
                Title is not null
                && Description is not null
                && Price?.Amount > 0
                && ApprovedBy is not null,
            _ => true
        };

        if (valid == false)
        {
            throw new InvalidEntityStateException(this, $"Post checks failed in state {State}");
        }
    }
}

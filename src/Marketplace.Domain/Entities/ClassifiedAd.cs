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

    public ClassifiedAd(ClassifiedAdId id, UserId ownerId) =>
        Apply(new Events.ClassifiedAdCreated(id, ownerId));

    public ClassifiedAdId Id { get; private set; } = null!;

    public UserId OwnerId { get; private set; } = null!;

    public ClassifiedAdTitle? Title { get; private set; }

    public ClassifiedAdDescription? Description { get; private set; }

    public Price? Price { get; private set; }

    public UserId? ApprovedBy { get; private set; }

    public AdState State { get; private set; }

    public void SetTitle(ClassifiedAdTitle title) =>
        Apply(new Events.ClassifiedAdTitleChanged(Id, title));

    public void UpdateDescription(ClassifiedAdDescription description) =>
        Apply(new Events.ClassifiedAdDescriptionUpdated(Id, description));

    public void UpdatePrice(Price price) =>
        Apply(new Events.ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.Code));

    public void RequestToPublish() =>
        Apply(new Events.ClassifiedAdSentForReview(Id));

    protected override void When(IEvent<ClassifiedAd> @event)
    {
        Action when = @event switch
        {
            Events.ClassifiedAdCreated e => () =>
            {
                (Id, OwnerId, State) =
                    (new ClassifiedAdId(e.Id), new UserId(e.OwnerId), AdState.Inactive);
            },
            Events.ClassifiedAdDescriptionUpdated e => () =>
            {
                Description = ClassifiedAdDescription.FromString(e.Description);
            },
            Events.ClassifiedAdPriceUpdated e  => () =>
            {
                Price = new Price(e.Price, e.CurrencyCode);
            },
            Events.ClassifiedAdSentForReview => () =>
            {
                State = AdState.PendingReview;
            },
            Events.ClassifiedAdTitleChanged e  => () =>
            {
                Title = ClassifiedAdTitle.FromString(e.Title);
            },
            _ => throw new ArgumentOutOfRangeException(nameof(@event))
        };
        when();
    }

    protected override void EnsureValidState()
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

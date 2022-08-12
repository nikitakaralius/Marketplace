using Marketplace.Domain.Exceptions;
using Marketplace.Domain.Rules;
using Marketplace.Domain.ValueObjects;
using Marketplace.Framework;

namespace Marketplace.Domain.Entities;

public sealed class ClassifiedAd : AggregateRoot
{
    public enum AdState
    {
        PendingReview,
        Active,
        Inactive,
        MarkedAsSold
    }

    private readonly List<Picture> _pictures = new();

    public ClassifiedAd(ClassifiedAdId id, UserId ownerId) =>
        Apply(new Events.ClassifiedAdCreated(id, ownerId));

    private ClassifiedAd() { }

    #region Properties

    public Guid DatabaseId { get; private set; }

    public ClassifiedAdId Id { get; private set; } = null!;

    public UserId OwnerId { get; private set; } = null!;

    public ClassifiedAdTitle Title { get; private set; } = ClassifiedAdTitle.None;

    public ClassifiedAdDescription Description { get; private set; } = ClassifiedAdDescription.None;

    public Price Price { get; private set; } = Price.None;

    public UserId ApprovedBy { get; private set; } = UserId.None;

    public AdState State { get; private set; }

    public IEnumerable<Picture> Pictures => _pictures;

    #endregion

    private Picture? FirstPicture => _pictures.FirstOrDefault();

    #region Events

    public void SetTitle(ClassifiedAdTitle title) =>
        Apply(new Events.ClassifiedAdTitleChanged(Id, title));

    public void UpdateDescription(ClassifiedAdDescription description) =>
        Apply(new Events.ClassifiedAdDescriptionUpdated(Id, description));

    public void UpdatePrice(Price price) =>
        Apply(new Events.ClassifiedAdPriceUpdated(Id, price.Amount, price.Currency.Code));

    public void RequestToPublish() =>
        Apply(new Events.ClassifiedAdSentForReview(Id));

    public void AddPicture(Uri pictureUri, PictureSize size) =>
        Apply(new Events.PictureAddedToClassifiedAd(
                  ClassifiedAdId: Id,
                  PictureId: new Guid(),
                  Url: pictureUri.ToString(),
                  Height: size.Height,
                  Width: size.Width,
                  Order: _pictures.Max(p => p.Order) + 1));

    public void ResizePicture(PictureId pictureId, PictureSize newSize)
    {
        var picture = FindPictureBy(pictureId);
        if (picture is null)
        {
            throw new InvalidOperationException("Cannot resize picture that does not belong to this ad");
        }

        picture.Resize(newSize);
    }

    #endregion

    protected override void When(IEvent eventHappened)
    {
        Action when = eventHappened switch
        {
            Events.ClassifiedAdCreated e => () =>
            {
                (Id, DatabaseId, OwnerId, State) =
                    (new ClassifiedAdId(e.Id), e.Id, new UserId(e.OwnerId), AdState.Inactive);
            },
            Events.ClassifiedAdDescriptionUpdated e => () =>
            {
                Description = ClassifiedAdDescription.FromString(e.Description);
            },
            Events.ClassifiedAdPriceUpdated e => () =>
            {
                Price = new Price(e.Price, e.CurrencyCode);
            },
            Events.ClassifiedAdSentForReview  => () =>
            {
                State = AdState.PendingReview;
            },
            Events.ClassifiedAdTitleChanged e => () =>
            {
                Title = ClassifiedAdTitle.FromString(e.Title);
            },
            Events.PictureAddedToClassifiedAd e => () =>
            {
                Picture picture = new(Apply);
                ApplyToEntity(picture, e);
                _pictures.Add(picture);
            },
            _ => throw new ArgumentOutOfRangeException(nameof(eventHappened))
        };
        when();
    }

    protected override void EnsureValidState()
    {
        bool valid = State switch
        {
            AdState.PendingReview =>
                Title != ClassifiedAdTitle.None
                && Description != ClassifiedAdDescription.None
                && Price.Amount > 0
                && FirstPicture.HasCorrectSize(),
            AdState.Active =>
                Title != ClassifiedAdTitle.None
                && Description != ClassifiedAdDescription.None
                && Price.Amount > 0
                && ApprovedBy != UserId.None
                && FirstPicture.HasCorrectSize(),
            _ => true
        };

        if (valid == false)
        {
            throw new InvalidEntityStateException(this, $"Post checks failed in state {State}");
        }
    }

    private Picture? FindPictureBy(PictureId id) =>
        Pictures.FirstOrDefault(p => p.Id == id);
}

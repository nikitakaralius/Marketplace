using Marketplace.Domain.ClassifiedAd.Rules;
using Marketplace.Domain.ClassifiedAd.ValueObjects;
using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile.ValueObjects;
using Marketplace.Infrastructure;
using static Marketplace.Domain.ClassifiedAd.Events;

namespace Marketplace.Domain.ClassifiedAd;

public sealed class ClassifiedAd : AggregateRoot<ClassifiedAdId>
{
    public enum AdState
    {
        PendingReview = 1,
        Active = 2,
        Inactive = 3,
        MarkedAsSold = 4
    }

    private readonly List<Picture> _pictures = new();

    private ClassifiedAd() {}

    #region Properties

    public Guid DatabaseId { get; private set; }

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

    public ClassifiedAd(ClassifiedAdId id, UserId ownerId) =>
        Apply(new ClassifiedAdCreated
        {
            Id = id,
            OwnerId = ownerId
        });

    public void SetTitle(ClassifiedAdTitle title) =>
        Apply(new ClassifiedAdTitleChanged
        {
            Id = Id,
            Title = title
        });

    public void UpdateDescription(ClassifiedAdDescription description) =>
        Apply(new ClassifiedAdDescriptionUpdated
        {
            Id = Id,
            Description = description
        });

    public void UpdatePrice(Price price) =>
        Apply(new ClassifiedAdPriceUpdated
        {
            Id = Id,
            Price = price.Amount,
            CurrencyCode = price.Currency.Code
        });

    public void RequestToPublish() =>
        Apply(new ClassifiedAdSentForReview
        {
            Id = Id
        });

    public void Publish(UserId approvedBy) =>
        Apply(new ClassifiedAdPublished
        {
            Id = Id,
            ApprovedBy = approvedBy,
            OwnerId = OwnerId
        });

    public void AddPicture(Uri pictureUri, PictureSize size) =>
        Apply(new PictureAddedToClassifiedAd
        {
            ClassifiedAdId = Id,
            PictureId = new Guid(),
            Url = pictureUri.ToString(),
            Height = size.Height,
            Width = size.Width,
            Order = _pictures.Max(p => p.Order) + 1
        });

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
        var @event = eventHappened as IEvent<ClassifiedAd>;
        Action when = @event switch
        {
            ClassifiedAdCreated e => () =>
            {
                (Id, DatabaseId, OwnerId, State) =
                    (new ClassifiedAdId(e.Id), e.Id, new UserId(e.OwnerId), AdState.Inactive);
            },
            ClassifiedAdDescriptionUpdated e => () =>
            {
                Description = ClassifiedAdDescription.FromString(e.Description);
            },
            ClassifiedAdPriceUpdated e => () => { Price = new Price(e.Price, e.CurrencyCode); },
            ClassifiedAdSentForReview  => () => { State = AdState.PendingReview; },
            ClassifiedAdTitleChanged e => () => { Title = ClassifiedAdTitle.FromString(e.Title); },
            PictureAddedToClassifiedAd e => () =>
            {
                Picture picture = new(Apply);
                ApplyToEntity(picture, e);
                _pictures.Add(picture);
            },
            ClassifiedAdPublished e => () =>
            {
                ApprovedBy = new UserId(e.ApprovedBy);
                State = AdState.Active;
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
                && ApprovedBy != UserId.None,
                //&& FirstPicture.HasCorrectSize(),
            _ => true
        };

        if (valid == false)
        {
            throw new DomainException.InvalidEntityState(this, $"Post checks failed in state {State}");
        }
    }

    private Picture? FindPictureBy(PictureId id) =>
        Pictures.FirstOrDefault(p => p.Id == id);
}

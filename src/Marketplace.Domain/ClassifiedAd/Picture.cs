using Marketplace.Domain.ClassifiedAd.ValueObjects;
using Marketplace.EventSourcing;

namespace Marketplace.Domain.ClassifiedAd;

public sealed class Picture : Entity
{
    public Picture(Action<IEvent> applier) : base(applier)
    {
    }

    private Picture() { }

    public Guid DatabaseId { get; private set; }

    public PictureId Id { get; private set; } = null!;

    public ClassifiedAdId ParentId { get; private set; } = null!;

    public PictureSize Size { get; private set; } = null!;

    public Uri? Location { get; private set; }

    public int Order { get; private set; }

    public void Resize(PictureSize newSize) =>
        Apply(new Events.PictureResized
        {
            PictureId = Id.Value,
            Height = newSize.Height,
            Width = newSize.Width
        });

    protected override void When(IEvent eventHappened)
    {
        var @event = eventHappened as IEvent<Picture>;
        Action when = @event switch
        {
            Events.PictureAddedToClassifiedAd e => () =>
            {
                ParentId = new(e.ClassifiedAdId);
                Id = new(e.PictureId);
                DatabaseId = e.PictureId;
                Size = new PictureSize
                {
                    Height = e.Height,
                    Width = e.Width
                };
                Location = new(e.Url);
                Order = e.Order;
            },
            Events.PictureResized e => () =>
            {
                Size = new PictureSize
                {
                    Height = e.Height,
                    Width = e.Width
                };
            },
            _ => throw new ArgumentOutOfRangeException(nameof(eventHappened))
        };
        when();
    }
}

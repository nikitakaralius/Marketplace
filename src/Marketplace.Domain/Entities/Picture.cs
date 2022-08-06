using Marketplace.Domain.ValueObjects;
using Marketplace.Framework;

namespace Marketplace.Domain.Entities;

public sealed class Picture : Entity
{
    public Picture(Action<IEvent> applier) : base(applier)
    {
    }

    internal PictureId Id { get; private set; } = null!;

    internal PictureSize Size { get; private set; } = null!;

    internal Uri? Location { get; private set; }

    internal int Order { get; private set; }

    protected override void When(IEvent eventHappened)
    {
        Action when = eventHappened switch
        {
            Events.PictureAddedToClassifiedAd e => () =>
            {
                Id = new(e.PictureId);
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

    public void Resize(PictureSize newSize) =>
        Apply(new Events.PictureResized(Id.Value, newSize.Height, newSize.Width));
}

using Marketplace.Domain.ValueObjects;
using Marketplace.Framework;

namespace Marketplace.Domain.Entities;

public sealed class Picture : Entity
{
    public PictureId Id { get; private set; }

    internal PictureSize Size { get; set; }

    internal Uri? Location { get; set; }

    internal int Order { get; set; }

    protected override void When(IEvent eventHappened)
    {
        throw new NotImplementedException();
    }

    protected override void EnsureValidState()
    {
        throw new NotImplementedException();
    }
}

using EventStore.ClientAPI;
using Marketplace.Domain.ClassifiedAd;
using Marketplace.Infrastructure.Store;
using static Marketplace.Projections.ClassifiedAdUpcastedEvents;

namespace Marketplace.Projections;

internal sealed class ClassifiedAdUpcasters : IProjection
{
    private const string StreamName = "UpcastedClassifiedAdEvents";

    private readonly IEventStoreConnection _connection;
    private readonly Func<Guid, string?> _getUserPhoto;

    public ClassifiedAdUpcasters(IEventStoreConnection connection, Func<Guid, string?> getUserPhoto)
    {
        _connection = connection;
        _getUserPhoto = getUserPhoto;
    }

    public Task ProjectAsync(IEvent @event)
    {
        Func<Task> project = @event switch
        {
            Events.ClassifiedAdPublished e => () =>
            {
                var newEvent = new V1.ClassifiedAdPublished
                {
                    Id = e.Id,
                    OwnerId = e.OwnerId,
                    ApprovedBy = e.ApprovedBy,
                    SellerPhotoUrl = _getUserPhoto(e.OwnerId) ?? ""
                };
                return _connection.AppendEventsAsync(StreamName, ExpectedVersion.Any, newEvent);
            },
            _ => () => Task.CompletedTask
        };

        return project();
    }
}

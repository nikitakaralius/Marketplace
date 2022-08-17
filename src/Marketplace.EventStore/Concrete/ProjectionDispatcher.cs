using Marketplace.EventStore.Extensions;
using Marketplace.EventStore.Serialization;
using Marketplace.Infrastructure.Checkpoints;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Marketplace.EventStore.Concrete;

internal sealed class ProjectionDispatcher
{
    private const string CheckpointName = "readmodels";

    private static readonly ILogger Logger = Log.ForContext<ProjectionDispatcher>();

    private readonly IEnumerable<IProjection> _projections;
    private readonly IEventStoreConnection _connection;
    private readonly ICheckpointStore _store;

    private EventStoreAllCatchUpSubscription _subscription = null!;

    public ProjectionDispatcher(IEventStoreConnection connection,
                                ICheckpointStore store,
                                IEnumerable<IProjection> projections)
    {
        _connection = connection;
        _store = store;
        _projections = projections;
    }

    public async Task StartAsync()
    {
        CatchUpSubscriptionSettings settings =
            new(2000, 500, Logger.IsEnabled(LogEventLevel.Verbose), false, "try-out-subscription");

        var checkpoint = await _store.CheckpointByNameAsync(CheckpointName);
        _subscription = _connection.SubscribeToAllFrom(checkpoint.ToPosition(), settings, EventAppeared);
    }

    public void Stop() => _subscription.Stop();

    private async Task EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent re)
    {
        Checkpoint ToCheckpoint(ResolvedEvent e) => e.OriginalPosition!.Value.ToCheckpoint(CheckpointName);

        bool IsEsInternal(ResolvedEvent e) => e.Event.EventType.StartsWith("$");

        if (IsEsInternal(re)) return;

        var @event = re.Deserialize();
        Logger.Debug("Projecting event {Type}", @event.GetType().Name);

        await Task.WhenAll(_projections.Select(x => x.ProjectAsync(@event)));
        await _store.StoreAsync(ToCheckpoint(re));
    }
}

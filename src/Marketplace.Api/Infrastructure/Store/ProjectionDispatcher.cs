using EventStore.ClientAPI;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Marketplace.Infrastructure.Store;

internal sealed class ProjectionDispatcher
{
    private static readonly ILogger Logger = Log.ForContext<ProjectionDispatcher>();

    private readonly IEnumerable<IProjection> _projections;
    private readonly IEventStoreConnection _connection;

    private EventStoreAllCatchUpSubscription _subscription = null!;

    public ProjectionDispatcher(IEventStoreConnection connection, params IProjection[] projections)
    {
        _connection = connection;
        _projections = projections;
    }

    public void Start()
    {
        CatchUpSubscriptionSettings settings =
            new(2000, 500, Logger.IsEnabled(LogEventLevel.Verbose), true, "try-out-subscription");

        _subscription = _connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
    }

    public void Stop() => _subscription.Stop();

    private Task EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent re)
    {
        bool IsEsInternal(ResolvedEvent e) => e.Event.EventType.StartsWith("$");

        if (IsEsInternal(re)) return Task.CompletedTask;

        var @event = re.Deserialize();
        Logger.Debug("Projecting event {Type}", @event.GetType().Name);
        return Task.WhenAll(_projections.Select(x => x.ProjectAsync(@event)));
    }
}

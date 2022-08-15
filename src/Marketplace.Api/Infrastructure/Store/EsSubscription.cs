using EventStore.ClientAPI;
using Marketplace.Domain.ClassifiedAd;
using Serilog;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Marketplace.Infrastructure.Store;

internal sealed class EsSubscription
{
    private readonly IEventStoreConnection _connection;
    private static readonly ILogger Logger = Log.ForContext<EsSubscription>();
    private readonly IList<ReadModels.ClassifiedAdDetails> _items;

    private EventStoreAllCatchUpSubscription _subscription = null!;

    public EsSubscription(IEventStoreConnection connection,
                          IList<ReadModels.ClassifiedAdDetails> items)
    {
        _connection = connection;
        _items = items;
    }

    public void Start()
    {
        CatchUpSubscriptionSettings settings =
            new(2000, 500, Logger.IsEnabled(LogEventLevel.Verbose), true, "try-out-subscription");

        _subscription = _connection.SubscribeToAllFrom(Position.Start, settings, EventAppeared);
    }

    public void Stop() => _subscription.Stop();

    private Task EventAppeared(EventStoreCatchUpSubscription subscription, ResolvedEvent ev)
    {
        bool IsEsInternal(ResolvedEvent resolvedEvent) => resolvedEvent.Event.EventType.StartsWith("$");

        if (IsEsInternal(ev)) return Task.CompletedTask;

        var @event = ev.Deserialize() as IEvent<ClassifiedAd>;

        Logger.Debug("Projecting event {Type}", @event?.GetType().Name);

        Action project = @event switch
        {
            Events.ClassifiedAdCreated e => () =>
            {
                _items.Add(new ReadModels.ClassifiedAdDetails
                {
                    Id = e.Id
                });
            },
            Events.ClassifiedAdDescriptionUpdated e => () =>
            {
                UpdateItem(e.Id, x => { x.Description = e.Description; });
            },
            Events.ClassifiedAdPriceUpdated e => () =>
            {
                UpdateItem(e.Id, x =>
                {
                    x.Price = e.Price;
                    x.CurrencyCode = e.CurrencyCode;
                });
            },
            Events.ClassifiedAdTitleChanged e => () => { UpdateItem(e.Id, x => { x.Title = e.Title; }); },
            _                                 => throw new ArgumentOutOfRangeException(nameof(@event))
        };

        project();

        return Task.CompletedTask;
    }

    private void UpdateItem(Guid id, Action<ReadModels.ClassifiedAdDetails> update)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item is null) return;
        update(item);
    }
}

using Marketplace.Infrastructure.Checkpoints;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EventStore.Appliers;

internal sealed class CheckpointStoreApplier : ScopeIsolatedApplier<ICheckpointStore>
{
    public CheckpointStoreApplier(IServiceScopeFactory factory) : base(factory)
    {
    }
}

using Marketplace.ClassifiedAds.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EventStore.Appliers;

internal sealed class AdRepositoryApplier : ScopeIsolatedApplier<IClassifiedAdRepository>
{
    public AdRepositoryApplier(IServiceScopeFactory factory) : base(factory)
    {
    }
}

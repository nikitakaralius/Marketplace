using Marketplace.Users.Projections;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EventStore.Appliers;

internal sealed class UserRepositoryApplier : ScopeIsolatedApplier<IUserRepository>
{
    public UserRepositoryApplier(IServiceScopeFactory factory) : base(factory)
    {
    }
}

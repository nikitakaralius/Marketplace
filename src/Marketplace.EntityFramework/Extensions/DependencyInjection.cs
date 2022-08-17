using Marketplace.EntityFramework.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EntityFramework.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
                                                    Action<PersistenceOptions> configure)
    {
        PersistenceOptions options = new();
        configure(options);

        services.AddDbContext<MarketplaceDbContext>(
            o => o.UseNpgsql(options.PostgresConnectionString));

        return services;
    }
}

public sealed class PersistenceOptions
{
    public string PostgresConnectionString { get; set; } = "";

    public string EventStoreConnectionString { get; set; } = "";
}

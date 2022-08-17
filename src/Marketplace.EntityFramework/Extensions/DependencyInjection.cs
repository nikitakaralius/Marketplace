using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.EntityFramework.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddEntityFrameworkModule(this IServiceCollection services,
                                                    Action<EntityFrameworkOptions> configure)
    {
        EntityFrameworkOptions options = new();
        configure(options);

        services.AddDbContext<MarketplaceDbContext>(
            o => o.UseNpgsql(options.PostgresConnectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
        services.AddScoped<ICheckpointStore, CheckpointStore>();

        return services;
    }
}

public sealed class EntityFrameworkOptions
{
    public string PostgresConnectionString { get; set; } = "";
}

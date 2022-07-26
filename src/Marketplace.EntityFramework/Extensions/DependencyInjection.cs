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
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}

public sealed class EntityFrameworkOptions
{
    public string PostgresConnectionString { get; private set; } = "";

    public void UsePostgres(string connectionString) =>
        PostgresConnectionString = connectionString;
}

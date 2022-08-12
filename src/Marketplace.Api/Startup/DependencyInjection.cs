using Marketplace.ClassifiedAds;
using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.Shared;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Marketplace.Infrastructure.Common;
using Marketplace.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MarketplaceDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("Postgres");
            options.UseNpgsql(connectionString);
        });

        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();

        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
        services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
        services.AddScoped<ClassifiedAdsApplicationService>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1",
                         new()
                         {
                             Title = "Classified Ads",
                             Version = "v1"
                         });
        });

        return services;
    }
}

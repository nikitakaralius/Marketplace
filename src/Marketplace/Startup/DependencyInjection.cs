using Marketplace.Domain.Services;
using Marketplace.Framework;
using Marketplace.Infrastructure;
using Marketplace.Infrastructure.EntityFramework;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
        services.AddScoped<ClassifiedAdsApplicationService>();
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

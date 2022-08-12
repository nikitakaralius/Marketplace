using Marketplace.Domain.Services;
using Marketplace.Infrastructure;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<ClassifiedAdsApplicationService>();
        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
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

using Marketplace.Domain.Shared;
using Marketplace.Infrastructure.Common;
using Marketplace.Infrastructure.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient(nameof(Constants.PurgoMalum), client =>
        {
            client.BaseAddress = new("https://www.purgomalum.com/service/containsprofanity");
        });

        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
        services.AddSingleton<IRequestHandler, SafeRequestHandler>();

        services.AddScoped<ClassifiedAdsApplicationService>();
        services.AddScoped<UserProfilesApplicationService>();

        services.AddTransient<IContentModeration, PurgoMalumContentModeration>();

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

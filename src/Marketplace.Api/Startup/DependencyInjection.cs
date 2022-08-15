using EventStore.ClientAPI;
using Marketplace.Domain.Shared;
using Marketplace.Infrastructure.Common;
using Marketplace.Infrastructure.Services;
using Marketplace.Infrastructure.Store;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration,
                                                       IWebHostEnvironment env)
    {
        services.AddHttpClient(nameof(Constants.PurgoMalum), client =>
        {
            client.BaseAddress = new("https://www.purgomalum.com/service/containsprofanity");
        });

        var esConnection = EventStoreConnection.Create(
            configuration.GetConnectionString("EventStore"),
            ConnectionSettings.Create().KeepReconnecting(),
            env.ApplicationName);

        services.AddSingleton(esConnection);
        services.AddSingleton<IAggregateStore, EsAggregateStore>();
        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
        services.AddSingleton<IRequestHandler, SafeRequestHandler>();

        services.AddScoped<ClassifiedAdsApplicationService>();
        services.AddScoped<UserProfilesApplicationService>();

        services.AddTransient<IContentModeration, PurgoMalumContentModeration>();

        services.AddHostedService<EventStoreService>();

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

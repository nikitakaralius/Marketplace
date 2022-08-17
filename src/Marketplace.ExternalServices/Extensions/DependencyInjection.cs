using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.ExternalServices.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalServicesModule(this IServiceCollection services)
    {
        services.AddHttpClient(nameof(Constants.PurgoMalum),
                               client =>
                               {
                                   client.BaseAddress = new(Constants.PurgoMalumUrl);
                               });

        services.AddTransient<IContentModeration, PurgoMalumContentModeration>();
        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();

        return services;
    }
}

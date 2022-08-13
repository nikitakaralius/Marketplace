using Marketplace.Domain.ClassifiedAd;
using Marketplace.Domain.Shared;
using Marketplace.Domain.UserProfile;
using Marketplace.Infrastructure.Common;
using Marketplace.Infrastructure.EntityFramework;
using Marketplace.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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

        services.AddDbContext<MarketplaceDbContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("Postgres");
            options.UseNpgsql(connectionString);
        });

        services.AddSingleton<ICurrencyLookup, FixedCurrencyLookup>();
        services.AddSingleton<IRequestHandler, SafeRequestHandler>();

        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
        services.AddScoped<IClassifiedAdRepository, ClassifiedAdRepository>();
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
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

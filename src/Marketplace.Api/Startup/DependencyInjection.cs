// ReSharper disable once CheckNamespace

using Marketplace.EntityFramework.Extensions;
using Marketplace.EventStore.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration,
                                                       IWebHostEnvironment env)
    {
        services.AddEntityFrameworkModule(opt =>
        {
            string connectionString = configuration.GetConnectionString("Postgres");
            opt.UsePostgres(connectionString);
        });

        services.AddEventStoreModule(opt =>
        {
            opt.ConnectionString = configuration.GetConnectionString("EventStore");
            opt.ConnectionName = env.ApplicationName;
        });


        services.AddSingleton<IRequestHandler, SafeRequestHandler>();
        services.AddScoped<ClassifiedAdsApplicationService>();
        services.AddScoped<UserProfilesApplicationService>();


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

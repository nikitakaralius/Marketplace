using EventStore.ClientAPI;
using Marketplace.Infrastructure.Persistence;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

internal static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
                                                       IConfiguration configuration,
                                                       IWebHostEnvironment env)
    {


        services.AddEventStore(configuration, env);

        services.AddSingleton<IAggregateStore, EsAggregateStore>();
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

    private static IServiceCollection AddEventStore(this IServiceCollection services,
                                                    IConfiguration configuration,
                                                    IWebHostEnvironment env)
    {
        var esConnection = EventStoreConnection.Create(
            configuration.GetConnectionString("EventStore"),
            ConnectionSettings.Create().KeepReconnecting(),
            env.ApplicationName);

        if (env.IsDevelopment())
        {
            List<ReadModels.ClassifiedAdDetails> adDetails = new();
            List<ReadModels.UserDetails> userDetails = new();

            ProjectionDispatcher dispatcher = new(
                esConnection,
                new ClassifiedAdDetailsProjection(
                    adDetails,
                    id => userDetails.FirstOrDefault(x => x.Id == id)?.DisplayName),
                new UserDetailsProjection(userDetails),
                new ClassifiedAdUpcasters(
                    esConnection,
                    id => userDetails.FirstOrDefault(x => x.Id == id)?.PhotoUrl));

            services.AddSingleton(dispatcher);

            services.AddSingleton<IEnumerable<ReadModels.ClassifiedAdDetails>>(adDetails);
            services.AddSingleton<IEnumerable<ReadModels.UserDetails>>(userDetails);
        }
        else
        {
            throw new InvalidOperationException("In-memory storage is not allowed in production");
        }

        services.AddSingleton(esConnection);
        services.AddHostedService<EventStoreService>();

        return services;
    }
}

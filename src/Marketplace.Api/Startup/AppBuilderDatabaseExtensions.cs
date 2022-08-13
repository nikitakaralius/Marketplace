// ReSharper disable once CheckNamespace

using Marketplace.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

internal static class AppBuilderDatabaseExtensions
{
    public static void EnsureDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MarketplaceDbContext>();

        if (context.Database.EnsureCreated() == false)
            context.Database.Migrate();
    }
}

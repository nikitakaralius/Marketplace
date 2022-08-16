using Marketplace.Persistence.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Marketplace.Persistence.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureDatabase(this IApplicationBuilder app)
    {
        var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MarketplaceDbContext>();

        if (context.Database.EnsureCreated() == false)
            context.Database.Migrate();
    }
}

// ReSharper disable once CheckNamespace

using Marketplace.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

internal static class AppBuilderDatabaseExtensions
{
    public static void EnsureDatabase(this IApplicationBuilder app)
    {
        var context = app.ApplicationServices.GetRequiredService<ClassifiedAdDbContext>();

        if (context.Database.EnsureCreated() == false)
            context.Database.Migrate();
    }
}

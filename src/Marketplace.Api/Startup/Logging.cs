using Serilog;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Logging;

internal static class Logging
{
    public static void AddSerilog(this ILoggingBuilder builder)
    {
        builder.ClearProviders();
        var logger = new LoggerConfiguration()
                     .WriteTo.Console()
                     .MinimumLevel.Debug()
                     .CreateLogger();
        builder.AddSerilog(logger);
    }
}

using Serilog;

Log.Logger = new LoggerConfiguration()
             .WriteTo.Console()
             .MinimumLevel.Verbose()
             .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

app.MapDefaultControllerRoute();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "Classified Ads V1");
});

app.Run();

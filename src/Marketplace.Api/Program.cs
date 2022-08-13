using Marketplace.ClassifiedAds;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddSerilog();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.EnsureDatabase();

app.MapClassifiedAdsCommandsApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint(
        url: "/swagger/v1/swagger.json",
        name: "Classified Ads V1");
});

app.Run();

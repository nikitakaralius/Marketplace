using Marketplace.Domain.Shared;
using Microsoft.AspNetCore.WebUtilities;

namespace Marketplace.Infrastructure.Services;

internal sealed class PurgoMalumContentModeration : IContentModeration
{
    private const string Url = "https://www.purgomalum.com/service/containsprofanity";
    private readonly IHttpClientFactory _factory;

    public PurgoMalumContentModeration(IHttpClientFactory factory) => _factory = factory;


    public bool CheckForProfanityAsync(string text) =>
        CheckForProfanity(text)
            .GetAwaiter()
            .GetResult();

    private async Task<bool> CheckForProfanity(string text)
    {
        string query = QueryHelpers.AddQueryString(Url, "text", text);
        string response = await _factory.CreateClient(nameof(Constants.PurgoMalum))
                                        .GetStringAsync(query);
        return bool.Parse(response);
    }
}

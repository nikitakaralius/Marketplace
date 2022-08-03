using Marketplace.Framework;
using static Marketplace.Contracts.ClassifiedAds;

namespace Marketplace.Api;

public sealed class ClassifiedAdsApplicationService : IApplicationService<V1.ICommand>
{
    public async Task HandleAsync(V1.ICommand command)
    {

    }
}

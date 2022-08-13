namespace Marketplace.Domain.Shared;

public interface IContentModeration
{
    bool CheckForProfanityAsync(string text);
}

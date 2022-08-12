namespace Marketplace.Domain.ClassifiedAd.Rules;

internal static class PictureRules
{
    public static bool HasCorrectSize(this Picture? picture) =>
        picture is not null
        && picture.Size.Height >= 800
        && picture.Size.Width >= 600;
}

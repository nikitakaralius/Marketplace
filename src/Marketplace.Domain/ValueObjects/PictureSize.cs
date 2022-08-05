namespace Marketplace.Domain.ValueObjects;

public sealed class PictureSize
{
    public PictureSize(int height, int width)
    {
        if (height <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(height),
                "Picture height must be a natural number");
        }

        if (width <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(width),
                "Picture width must be a natural number");
        }

        Height = height;
        Width = width;
    }

    internal PictureSize() { }

    public int Height { get; internal set; }

    public int Width { get; internal set; }
}

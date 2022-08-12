using Marketplace.Domain.ClassifiedAd.ValueObjects;

namespace Marketplace.Tests.ValueObjects;

public sealed class ClassifiedAdTitleTests
{
    [Fact]
    public void ObjectsWithTheSameTitleShouldBeEqual()
    {
        var title1 = ClassifiedAdTitle.FromString("Hello, World!");
        var title2 = ClassifiedAdTitle.FromString("Hello, World!");
        title1.Should().Be(title2);
    }
}

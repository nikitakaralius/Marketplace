namespace Marketplace.Tests.Entities;

public sealed class ClassifiedAdTests
{
    private sealed class FakeCurrencyLookup : ICurrencyLookup
    {
        public CurrencyDetails FindCurrency(string currencyCode) =>
            currencyCode switch
            {
                "USD" => new() {CurrencyCode = "USD", DecimalPlaces = 2},
                "EUR" => new() {CurrencyCode = "EUR", DecimalPlaces = 2},
                "JPY" => new() {CurrencyCode = "JPY", DecimalPlaces = 0},
                _     => CurrencyDetails.None
            };
    }

    private static readonly ICurrencyLookup CurrencyLookup = new FakeCurrencyLookup();

    private readonly ClassifiedAd _classifiedAd;

    public ClassifiedAdTests()
    {
        _classifiedAd = new ClassifiedAd(
            new ClassifiedAdId(Guid.NewGuid()),
            new UserId(Guid.NewGuid()));
    }

    [Fact]
    public void CanPublishValidAd()
    {
        _classifiedAd.SetTitle(
            ClassifiedAdTitle.FromString("Road Bicycle"));
        _classifiedAd.UpdateDescription(
            ClassifiedAdDescription.FromString("An amazing speedy bicycle"));
        _classifiedAd.UpdatePrice(
            Price.FromDecimal(400, "EUR", CurrencyLookup));
        var publish = () => _classifiedAd.RequestToPublish();
        publish.Should().NotThrow();
    }

    [Fact]
    public void CannotPublishWithoutTitle()
    {
        _classifiedAd.UpdateDescription(
            ClassifiedAdDescription.FromString("An amazing speedy bicycle"));
        _classifiedAd.UpdatePrice(
            Price.FromDecimal(400, "EUR", CurrencyLookup));
        var publish = () => _classifiedAd.RequestToPublish();
        publish.Should().ThrowExactly<InvalidEntityStateException>();
    }

    [Fact]
    public void CannotPublishWithoutDescription()
    {
        _classifiedAd.SetTitle(
            ClassifiedAdTitle.FromString("Road Bicycle"));
        _classifiedAd.UpdatePrice(
            Price.FromDecimal(400, "EUR", CurrencyLookup));
        var publish = () => _classifiedAd.RequestToPublish();
        publish.Should().ThrowExactly<InvalidEntityStateException>();
    }

    [Fact]
    public void CannotPublishWithoutPrice()
    {
        _classifiedAd.SetTitle(
            ClassifiedAdTitle.FromString("Road Bicycle"));
        _classifiedAd.UpdateDescription(
            ClassifiedAdDescription.FromString("An amazing speedy bicycle"));
        var publish = () => _classifiedAd.RequestToPublish();
        publish.Should().ThrowExactly<InvalidEntityStateException>();
    }

    [Fact]
    public void CannotPublishWithZeroPrice()
    {
        _classifiedAd.SetTitle(
            ClassifiedAdTitle.FromString("Road Bicycle"));
        _classifiedAd.UpdateDescription(
            ClassifiedAdDescription.FromString("An amazing speedy bicycle"));
        _classifiedAd.UpdatePrice(
            Price.FromDecimal(0, "EUR", CurrencyLookup));
        var publish = () => _classifiedAd.RequestToPublish();
        publish.Should().ThrowExactly<InvalidEntityStateException>();
    }
}

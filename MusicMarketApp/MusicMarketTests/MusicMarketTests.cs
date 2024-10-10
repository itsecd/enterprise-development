using MusicMarketplace.Domain;

namespace MusicMarketplace.Tests;

public class MusicMarketTest : IClassFixture<MusicMarketFixture>
{
    private MusicMarketFixture _fixture;

    public MusicMarketTest(MusicMarketFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void VinylRecordsInfoTest()
    {
        var fixtureProduct = _fixture.FixtureProducts.ToList();
        var request = (from product in fixtureProduct
                       where (product.TypeOfCarrier == CarrierType.VinylRecord) && (product.Status == ProductStatus.Sold)
                       select product).Count();
        Assert.Equal(2, request);
    }

    [Fact]
    public void ProductBySeller()
    {
        var fixtureProduct = _fixture.FixtureProducts.ToList();
        var request = (from product in fixtureProduct
                       where product.Seller is { ShopName: "StopRobot" }
                       orderby product.Price
                       select product).Count();
        Assert.Equal(3, request);
    }

    [Fact]
    public void GoodDisksInfo()
    {
        var fixtureProduct = _fixture.FixtureProducts.ToList();
        var request = (from product in fixtureProduct
                       where (product.TypeOfCarrier == CarrierType.Disc) && (product.Status == ProductStatus.Sale)
                       && (product.PublicationType == PublicationType.Album) && (product.Creator == "Monetochka")
                       && product.MediaStatus is MediaStatus.New or MediaStatus.Excellent or MediaStatus.Good
                       select product).Count();

        Assert.Equal(1, request);
    }

    [Fact]
    public void AudioCarriersInfo()
    {
        var fixtureProduct = _fixture.FixtureProducts.ToList();

        var request0 = (from product in fixtureProduct
                        where (product.TypeOfCarrier == CarrierType.Disc) && (product.Status == ProductStatus.Sold)
                        select product).Count();

        var request1 = (from product in fixtureProduct
                        where (product.TypeOfCarrier == CarrierType.Cassette) && (product.Status == ProductStatus.Sold)
                        select product).Count();

        var request2 = (from product in fixtureProduct
                        where (product.TypeOfCarrier == CarrierType.VinylRecord) && (product.Status == ProductStatus.Sold)
                        select product).Count();

        Assert.Equal(2, request0);
        Assert.Equal(2, request1);
        Assert.Equal(2, request2);
    }

    [Fact]
    public void TopFiveTest()
    {
        var customers = _fixture.FixtureCustomers.ToList();

        var customerPurchases =
            from customer in customers
            from purchase in customer.Purchases
            from product in purchase.Products
            select new
            {
                customer.Id,
                PurchaseCost = purchase.Products.Sum(p => p.Price + p.Seller?.Price)
            };
        var customerAvgPurchases =
            from customerPurchase in customerPurchases
            group customerPurchase by customerPurchase.Id into customer
            select new
            {
                customer.Key,
                AvgCost = customer.Average(cust => cust.PurchaseCost)
            };
        var top5 = customerAvgPurchases.OrderBy(customer => customer.AvgCost).Take(5);
        var max = top5.Max(a => a.AvgCost);
        Assert.Equal(7240, max);
    }

    [Fact]
    public void SoldProductsInTwoWeeks()
    {
        var now = new DateTime(2023, 3, 15);

        var purchases = _fixture.FixturePurchases.ToList();

        var request = (from purchase in purchases
                       where purchase.Date >= now.AddDays(-14)
                       select new
                       {
                           seller = purchase.Products[0].Seller,
                           count = purchase.Products.Count
                       }).ToList();

        var selCount = (from sel in request
                        group sel by sel.seller.ShopName into g
                        select new
                        {
                            seller = g.Key,
                            count = g.Sum(x => x.count)
                        }).ToList();

        Assert.Equal(1, selCount[0].count);
        Assert.Equal(1, selCount[1].count);
    }
}

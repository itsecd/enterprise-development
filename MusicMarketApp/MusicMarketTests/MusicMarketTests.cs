namespace MusicMarketplace.Tests;

using System.Linq;

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
                       where (product.TypeOfCarrier == "vinyl record") && (product.Status == "sold")
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
                       where (product.TypeOfCarrier == "disc") && (product.Status == "sale") && (product.PublicationType == "album")
                       && (product.Creator == "Monetochka") && (product.MediaStatus == "new" || product.MediaStatus == "excellent" || product.MediaStatus == "good")
                       select product).Count();


        Assert.Equal(1, request);
    }



    [Fact]
    public void AidioCarriersInfo()
    {
        var fixtureProduct = _fixture.FixtureProducts.ToList();

        var request0 = (from product in fixtureProduct
                        where (product.TypeOfCarrier == "disc") && (product.Status == "sold")
                        select product).Count();

        var request1 = (from product in fixtureProduct
                        where (product.TypeOfCarrier == "cassette") && (product.Status == "sold")
                        select product).Count();

        var request2 = (from product in fixtureProduct
                        where (product.TypeOfCarrier == "vinyl record") && (product.Status == "sold")
                        select product).Count();

        Assert.Equal(2, request0);
        Assert.Equal(2, request1);
        Assert.Equal(2, request2);
    }


    [Fact]
    public void TopFiveTest()
    {
        var customers = _fixture.FixtureCustomers.ToList();
        var purchases = _fixture.FixturePurchases.ToList();
        var products = _fixture.FixtureProducts.ToList();
        var sellers = _fixture.FixtureSellers.ToList();

        var customerPurchases =
            from customer in customers
            from purchase in customer.Purchases
            from product in purchase.Products
            select new
            {
                customer.Id,
                PurchaseCost = purchase.Products.Sum(product => product.Price + product.Seller?.Price)
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
    public void SoldProducsInTwoWeeks()
    {
        var now = DateTime.Now;


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

        Assert.Equal(2, selCount[0].count);
        Assert.Equal(1, selCount[1].count);
    }


}

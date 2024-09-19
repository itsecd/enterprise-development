using CityPharmacyChainLibrary;

namespace CityPharmacyChainTest;

[TestClass]
public class PriceListEntryUnitTest
{
    public PriceListEntry PriceListEntry { get; set; }

    public PriceListEntryUnitTest()
    {
        PriceListEntry = new PriceListEntry(146.00, "STADA", "AO\"Нижфарм\"", PaymentType.Cash, DateTime.Parse("2024-08-08"));
    }

    [TestMethod]
    public void TestCreate()
    {
        Assert.IsTrue(145.99 < PriceListEntry.Price && PriceListEntry.Price < 146.01);
        Assert.AreEqual(PriceListEntry.Company, "STADA");
        Assert.AreEqual(PriceListEntry.Manufacturer, "AO\"Нижфарм\"");
        Assert.AreEqual(PriceListEntry.PaymentType, PaymentType.Cash);
        Assert.AreEqual(PriceListEntry.SaleDate, DateTime.Parse("2024-08-08"));
    }
}

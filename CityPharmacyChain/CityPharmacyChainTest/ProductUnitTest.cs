using CityPharmacyChainLibrary;

namespace CityPharmacyChainTest;

[TestClass]
public class ProductUnitTest
{
    public Product Product { get; set; }

    public ProductUnitTest()
    {
        Product = new Product(173655243, 10, "Гепариновая мазь", "Мазь для наружного применения");
        Product.PharmaceuticalGroup.Add("Антикоагулярное средство прямого действия для местного применения");
        Product.PharmaceuticalGroup.Add("Прочие препараты");
    }

    [TestMethod]
    public void TestCreate()
    {
        Assert.AreEqual(Product.Code, 173655243);
        Assert.AreEqual(Product.Count, 10);
        Assert.AreEqual(Product.Name, "Гепариновая мазь");
        Assert.AreEqual(Product.ProductGroup, "Мазь для наружного применения");
        Assert.AreEqual(Product.PharmaceuticalGroup[0], "Антикоагулярное средство прямого действия для местного применения");
        Assert.AreEqual(Product.PharmaceuticalGroup[1], "Прочие препараты");
    }
}

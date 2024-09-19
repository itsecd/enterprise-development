using CityPharmacyChainLibrary;

namespace CityPharmacyChainTest;

[TestClass]
public class PharmacyUnitTest
{
    public Pharmacy Pharmacy { get; set; }

    public PharmacyUnitTest()
    {
        Pharmacy = new Pharmacy(231, "Вита", 85466382313, "г. Самара, ул. Ново-Садовая, д. 36", "Иванов Иван Иванович");
    }

    [TestMethod]
    public void TestCreate()
    {
        Assert.AreEqual(Pharmacy.Number, 231);
        Assert.AreEqual(Pharmacy.Name, "Вита");
        Assert.AreEqual(Pharmacy.PhoneNumber, 85466382313);
        Assert.AreEqual(Pharmacy.Address, "г. Самара, ул. Ново-Садовая, д. 36");
        Assert.AreEqual(Pharmacy.Director, "Иванов Иван Иванович");
    }
}
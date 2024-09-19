using CityPharmacyChain.Domain;

namespace CityPharmacyChain.Tests
{
    public class PharmacyChainTest(PharmacyChainFixture fixture): IClassFixture<PharmacyChainFixture>
    {
        private PharmacyChainFixture _fixture = fixture;

        [Fact]
        public void TestSelectAllProductsForPharmacy()
        {
            var allProductsForPharmacy =
                (from pharmacy in _fixture.PharmacyList
                 join product in _fixture.ProductList on pharmacy.PharmacyNumber equals product.PharmacyNumber
                 orderby product.Name
                 where pharmacy.Name is "Апрель"
                 select new
                 {
                     product.ProductCode,
                     product.Name,
                     product.Count,
                     product.ProductGroup,
                 });
            Assert.Equal(allProductsForPharmacy,
                [
                    new { ProductCode = 1, Name = "Гепариновая мазь", Count = 10, ProductGroup = "Мазь для наружного применения" },
                    new { ProductCode = 2, Name = "Левомеколь", Count = 19, ProductGroup = "Мазь для наружного применения" },
                    new { ProductCode = 4, Name = "Нурофен таблетки", Count = 5, ProductGroup = "Таблетки, покрытые оболочкой" },
                    new { ProductCode = 8, Name = "Пенталгин Экстра гель", Count = 2, ProductGroup = "Гель для наружного применения" },
                    new { ProductCode = 5, Name = "Риностоп спрей", Count = 17, ProductGroup = "Спрей назальный" },
                    new { ProductCode = 6, Name = "Стрептоцид порошок", Count = 3, ProductGroup = "Порошок для наружного применения" },
                    new { ProductCode = 9, Name = "Тромбо АСС таблетки", Count = 12, ProductGroup = "Таблетки, покрытые оболочкой" },
                ]);
        }

        [Fact]
        public void TestSelectProductCount()
        {
            var productCount =
                from pharmacy in _fixture.PharmacyList
                join product in _fixture.ProductList on pharmacy.PharmacyNumber equals product.PharmacyNumber
                orderby product.Name
                where product.Name is "Левомеколь"
                select new
                {
                    pharmacy.Name,
                    product.Count,
                };
            Assert.Equal(productCount, 
                [
                    new { Name = "ВИТА", Count = 10 }, 
                    new { Name = "Апрель", Count = 19 }, 
                    new { Name = "Имплозия", Count = 3 },
                ]);
        }

        [Fact]
        public void TestSelectAvgProductPriceForEachPharmacy()
        {
            var avgProductPriceForEachPharmacy =
                from pharmaceuticalGroup in _fixture.PharmaceuticalGroups
                join product in _fixture.ProductList on pharmaceuticalGroup.ProductCode equals product.ProductCode
                join pharmacy in _fixture.PharmacyList on product.PharmacyNumber equals pharmacy.PharmacyNumber
                join priceListEntry in _fixture.PriceList on product.ProductCode equals priceListEntry.ProductCode
                group priceListEntry by new { PharmacyName = pharmacy.Name, pharmaceuticalGroup.Name, priceListEntry.Price } into result
                select new
                {
                    result.Key.PharmacyName,
                    result.Key.Name,
                    AveragePrice = result.Average(p => p.Price)
                };
            Assert.Equal("ВИТА", avgProductPriceForEachPharmacy.First().Name);
            Assert.Equal("Антикоагулянт", avgProductPriceForEachPharmacy.First().Name);
            Assert.True(145.9 < avgProductPriceForEachPharmacy.First().AveragePrice && avgProductPriceForEachPharmacy.First().AveragePrice < 146.1);
        }

        [Fact]
        public void TestSelectMaxProductSales()
        {
            var maxProductSales =
                from pharmacy in _fixture.PharmacyList
                join priceListEntry in _fixture.PriceList on pharmacy.PharmacyNumber equals priceListEntry.PharmacyNumber
                join product in _fixture.ProductList on priceListEntry.PharmacyNumber equals product.PharmacyNumber
                where product.Name == "Левомеколь" && (priceListEntry.SaleDate > DateTime.Parse("2024-08-15") && priceListEntry.SaleDate < DateTime.Parse("2024-09-20"))
                group product by new { pharmacy.Name, product.Count } into result
                select new
                {
                    result.Key.Name,
                    SaleCount = result.Count(),
                    SaleVolume = result.Sum(p => p.Count),
                };
            maxProductSales = 
                (from maxProductSale in maxProductSales
                orderby maxProductSale.SaleCount, maxProductSale.SaleVolume descending
                select maxProductSale).Take(5);
            Assert.Equal(maxProductSales,
                [
                    new { Name = "Апрель", SaleCount = 1, SaleVolume = 19 },
                    new { Name = "ВИТА", SaleCount = 1, SaleVolume = 10 },
                    new { Name = "Имплозия", SaleCount = 2, SaleVolume = 6 },
                ]);
        }
    }
}
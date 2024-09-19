using CityPharmacyChain.Tests;
using CityPharmacyChain.Domain;

namespace CityPharmacyChain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _fixture = new PharmacyChainFixture();

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
            foreach (var product in maxProductSales)
            {
                Console.WriteLine($"{product.Name} {product.SaleCount} {product.SaleVolume}");
            }
        }
    }
}

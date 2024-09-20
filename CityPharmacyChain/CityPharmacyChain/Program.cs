using CityPharmacyChain.Tests;
using CityPharmacyChain.Domain;
using System.Security.Cryptography;
using Xunit;

namespace CityPharmacyChain
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var _fixture = new PharmacyChainFixture();

            var tmpPharmaciesWithMinProductPrice =
                from pharmacy in _fixture.PharmacyList
                join priceListEntry in _fixture.PriceList on pharmacy.PharmacyNumber equals priceListEntry.PharmacyNumber
                join product in _fixture.ProductList on priceListEntry.ProductCode equals product.ProductCode
                where product.Name == "Левомеколь"
                group priceListEntry by priceListEntry.PharmacyNumber into result
                select new
                {
                    PharmacyNumber = result.Key,
                    SoldVolume = result.Min(p => p.Price),
                };
            var pharmaciesWithMinProductPrice =
                 from entry in tmpPharmaciesWithMinProductPrice
                 join pharmacy in _fixture.PharmacyList on entry.PharmacyNumber equals pharmacy.PharmacyNumber
                 let min = tmpPharmaciesWithMinProductPrice.Min(p => p.SoldVolume)
                 where entry.SoldVolume < min + 0.01 && entry.SoldVolume > min - 0.01
                 select new
                 {
                     pharmacy.Name,
                 };
            foreach (var product in pharmaciesWithMinProductPrice)
            {
                Console.WriteLine($"{product.Name}");
            }
        }
    }
}

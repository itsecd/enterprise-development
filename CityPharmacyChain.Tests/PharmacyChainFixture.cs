using CityPharmacyChain.Domain;

namespace CityPharmacyChain.Tests;

public class PharmacyChainFixture
{
    public List<PriceListEntry> PriceList { get; set; }
    public List<Pharmacy> PharmacyList { get; set; }
    public List<Product> ProductList { get; set; }
    public List<PharmaceuticalGroup> PharmaceuticalGroups { get; set; }
    public List<PharmacyProduct> PharmacyProductList { get; set; }

    public PharmacyChainFixture()
    {
        PharmacyList =
        [
            new Pharmacy(1, "VITA", 89456372837, "Samara, Novo-Sadovaya st., 36", "Ivanov Ivan Ivanovich"),
            new Pharmacy(2, "April", 86748356473, "Samara, Lenin ave., 6", "Sergeev Gennady Vasilievich"),
            new Pharmacy(3, "BE HEALTHY!", 87443856936, "Samara, Lenin ave., 14", "Andreeva Nadezhda Ivanovna"),
            new Pharmacy(4, "Implosion", 89975362563, "Samara, Lenin ave., 6", "Petrov Petr Sergeevich"),
        ];

        PriceList =
        [
            new PriceListEntry(1, 1, 146, 1, "JSC Nizhpharm", PaymentType.Cashless, DateTime.Parse("2024-08-01")),
            new PriceListEntry(2, 1, 198, 2,  "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new PriceListEntry(5, 1, 110, 3, "Pharmstandard-medicines", PaymentType.Cash, DateTime.Parse("2024-07-24")),
            new PriceListEntry(1, 1, 146, 2, "JSC Nizhpharm", PaymentType.Cashless, DateTime.Parse("2024-08-01")),
            new PriceListEntry(2, 1, 198, 3, "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new PriceListEntry(5, 1, 110, 1, "Pharmstandard-medicines", PaymentType.Cash, DateTime.Parse("2024-07-24")),

            new PriceListEntry(3, 2, 99, 1, "JSC Tula Pharmaceutical Factory", PaymentType.Cashless, DateTime.Parse("2024-09-05")),
            new PriceListEntry(4, 2, 211, 2, "JSC Reckitt Benckiser", PaymentType.Cashless, DateTime.Parse("2024-08-11")),
            new PriceListEntry(3, 2, 99, 3, "JSC Tula Pharmaceutical Factory", PaymentType.Cashless, DateTime.Parse("2024-09-05")),
            new PriceListEntry(4, 2, 211, 2, "JSC Reckitt Benckiser", PaymentType.Cashless, DateTime.Parse("2024-08-11")),

            new PriceListEntry(2, 3, 190, 3, "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new PriceListEntry(6, 3, 150, 4, "JSC Doctor", PaymentType.Cashless, DateTime.Parse("2024-09-01")),
            new PriceListEntry(8, 3,382, 2, "JSC Lecco", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new PriceListEntry(6, 3, 150, 3, "JSC Doctor", PaymentType.Cashless, DateTime.Parse("2024-09-01")),
            new PriceListEntry(8, 3, 382, 1, "JSC Lekko", PaymentType.Cash, DateTime.Parse("2024-08-30")),

            new PriceListEntry(2, 4, 190, 4, "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new PriceListEntry(2, 4, 190, 3, "JSC Nizhpharm", PaymentType.Cashless, DateTime.Parse("2024-09-14")),
            new PriceListEntry(9, 4, 144, 4, "Jelfa S A", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new PriceListEntry(9, 4, 144, 5, "Jelfa S A", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new PriceListEntry(7, 4, 139, 1, "JSC Lekko", PaymentType.Cash, DateTime.Parse("2024-09-03")),
            new PriceListEntry(7, 4, 139, 1, "JSC Lekko", PaymentType.Cash, DateTime.Parse("2024-09-03")),
        ];

        PharmaceuticalGroups =
        [
            new PharmaceuticalGroup(1, "Anticoagulant"),
            new PharmaceuticalGroup(2, "Antibacterial agent"),
            new PharmaceuticalGroup(2, "Desinfectant"),
            new PharmaceuticalGroup(3, "Liniment"),
            new PharmaceuticalGroup(4, "Nonsteroidal anti-inflammatory drug"),
            new PharmaceuticalGroup(5, "Vasoconstrictor drug"),
            new PharmaceuticalGroup(6, "Antibacterial agent"),
            new PharmaceuticalGroup(7, "Antibacterial agent"),
            new PharmaceuticalGroup(8, "Analgesic agent"),
            new PharmaceuticalGroup(8, "Nonsteroidal anti-inflammatory drug"),
            new PharmaceuticalGroup(8, "Psychostimulant"),
            new PharmaceuticalGroup(9, "Nonsteroidal anti-inflammatory drug"),
        ];

        ProductList =
        [
            new Product(1, "Heparin ointment", "Ointment for external use"),
            new Product(2, "Levomekol", "Ointment for external use"),
            new Product(3, "Vishnevsky ointment", "Ointment for external use"),
            new Product(4, "Nurofen", "Pills"),
            new Product(5, "Rinostop", "Nasal spray"),
            new Product(6, "Streptocide", "Powder for external use"),
            new Product(7, "Sodium sulfacyl", "Капли глазные"),
            new Product(8, "Pentalgin", "Gel for external use"),
            new Product(9, "Trombo", "Pills"),
        ];

        PharmacyProductList =
        [
            new PharmacyProduct(1, 1, 15),
            new PharmacyProduct(2, 1, 10),
            new PharmacyProduct(3, 1, 20),
            new PharmacyProduct(4, 1, 12),
            new PharmacyProduct(5, 1, 5),
            new PharmacyProduct(6, 1, 8),
            new PharmacyProduct(7, 1, 17),
            new PharmacyProduct(8, 1, 10),
            new PharmacyProduct(9, 1, 15),

            new PharmacyProduct(1, 2, 10),
            new PharmacyProduct(2, 2, 19),
            new PharmacyProduct(4, 2, 6),
            new PharmacyProduct(5, 2, 17),
            new PharmacyProduct(6, 2, 3),
            new PharmacyProduct(8, 2, 2),
            new PharmacyProduct(9, 2, 12),

            new PharmacyProduct(1, 3, 10),
            new PharmacyProduct(5, 3, 4),
            new PharmacyProduct(6, 3, 7),
            new PharmacyProduct(7, 3, 17),
            new PharmacyProduct(8, 3, 14),
            new PharmacyProduct(9, 3, 5),

            new PharmacyProduct(1, 4, 12),
            new PharmacyProduct(2, 4, 3),
            new PharmacyProduct(3, 4, 20),
            new PharmacyProduct(4, 4, 12),
            new PharmacyProduct(5, 4, 16),
            new PharmacyProduct(6, 4, 10),
            new PharmacyProduct(7, 4, 11),
        ];
    }
}

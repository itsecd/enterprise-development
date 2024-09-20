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
            new(1, "VITA", 89456372837, "Samara, Novo-Sadovaya st., 36", "Ivanov Ivan Ivanovich"),
            new(2, "April", 86748356473, "Samara, Lenin ave., 6", "Sergeev Gennady Vasilievich"),
            new(3, "BE HEALTHY!", 87443856936, "Samara, Lenin ave., 14", "Andreeva Nadezhda Ivanovna"),
            new(4, "Implosion", 89975362563, "Samara, Lenin ave., 6", "Petrov Petr Sergeevich"),
        ];

        PriceList =
        [
            new(1, 1, 146, 1, "JSC Nizhpharm", PaymentType.Cashless, DateTime.Parse("2024-08-01")),
            new(2, 1, 198, 2,  "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(5, 1, 110, 3, "Pharmstandard-medicines", PaymentType.Cash, DateTime.Parse("2024-07-24")),
            new(1, 1, 146, 2, "JSC Nizhpharm", PaymentType.Cashless, DateTime.Parse("2024-08-01")),
            new(2, 1, 198, 3, "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(5, 1, 110, 1, "Pharmstandard-medicines", PaymentType.Cash, DateTime.Parse("2024-07-24")),

            new(3, 2, 99, 1, "JSC Tula Pharmaceutical Factory", PaymentType.Cashless, DateTime.Parse("2024-09-05")),
            new(4, 2, 211, 2, "JSC Reckitt Benckiser", PaymentType.Cashless, DateTime.Parse("2024-08-11")),
            new(3, 2, 99, 3, "JSC Tula Pharmaceutical Factory", PaymentType.Cashless, DateTime.Parse("2024-09-05")),
            new(4, 2, 211, 2, "JSC Reckitt Benckiser", PaymentType.Cashless, DateTime.Parse("2024-08-11")),

            new(2, 3, 190, 3, "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(6, 3, 150, 4, "JSC Doctor", PaymentType.Cashless, DateTime.Parse("2024-09-01")),
            new(8, 3,382, 2, "JSC Lecco", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new(6, 3, 150, 3, "JSC Doctor", PaymentType.Cashless, DateTime.Parse("2024-09-01")),
            new(8, 3, 382, 1, "JSC Lekko", PaymentType.Cash, DateTime.Parse("2024-08-30")),

            new(2, 4, 190, 4, "JSC Nizhpharm", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(2, 4, 190, 3, "JSC Nizhpharm", PaymentType.Cashless, DateTime.Parse("2024-09-14")),
            new(9, 4, 144, 4, "Jelfa S A", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new(9, 4, 144, 5, "Jelfa S A", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new(7, 4, 139, 1, "JSC Lekko", PaymentType.Cash, DateTime.Parse("2024-09-03")),
            new(7, 4, 139, 1, "JSC Lekko", PaymentType.Cash, DateTime.Parse("2024-09-03")),
        ];

        PharmaceuticalGroups =
        [
            new(1, "Anticoagulant"),
            new(2, "Antibacterial agent"),
            new(2, "Desinfectant"),
            new(3, "Liniment"),
            new(4, "Nonsteroidal anti-inflammatory drug"),
            new(5, "Vasoconstrictor drug"),
            new(6, "Antibacterial agent"),
            new(7, "Antibacterial agent"),
            new(8, "Analgesic agent"),
            new(8, "Nonsteroidal anti-inflammatory drug"),
            new(8, "Psychostimulant"),
            new(9, "Nonsteroidal anti-inflammatory drug"),
        ];

        ProductList =
        [
            new(1, "Heparin ointment", "Ointment for external use"),
            new(2, "Levomekol", "Ointment for external use"),
            new(3, "Vishnevsky ointment", "Ointment for external use"),
            new(4, "Nurofen", "Pills"),
            new(5, "Rinostop", "Nasal spray"),
            new(6, "Streptocide", "Powder for external use"),
            new(7, "Sodium sulfacyl", "Капли глазные"),
            new(8, "Pentalgin", "Gel for external use"),
            new(9, "Trombo", "Pills"),
        ];

        PharmacyProductList =
        [
            new(1, 1, 15),
            new(2, 1, 10),
            new(3, 1, 20),
            new(4, 1, 12),
            new(5, 1, 5),
            new(6, 1, 8),
            new(7, 1, 17),
            new(8, 1, 10),
            new(9, 1, 15),

            new(1, 2, 10),
            new(2, 2, 19),
            new(4, 2, 6),
            new(5, 2, 17),
            new(6, 2, 3),
            new(8, 2, 2),
            new(9, 2, 12),

            new(1, 3, 10),
            new(5, 3, 4),
            new(6, 3, 7),
            new(7, 3, 17),
            new(8, 3, 14),
            new(9, 3, 5),

            new(1, 4, 12),
            new(2, 4, 3),
            new(3, 4, 20),
            new(4, 4, 12),
            new(5, 4, 16),
            new(6, 4, 10),
            new(7, 4, 11),
        ];
    }
}

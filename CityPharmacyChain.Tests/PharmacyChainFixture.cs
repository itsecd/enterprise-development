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
            new(1, "ВИТА", 89456372837, "г. Самара, ул. Ново-Садовая, д. 36", "Иванов Иван Иванович"),
            new(2, "Апрель", 86748356473, "г. Самара, пр-т. Ленина, д. 6", "Сергеев Геннадий Васильевич"),
            new(3, "БУДЬ ЗДОРОВ!", 87443856936, "г. Самара, пр-т. Ленина, д. 14", "Андреева Надежда Ивановна"),
            new(4, "Имплозия", 89975362563, "г. Самара, пр-т. Ленина, д. 6", "Петров Павел Сергеевич"),
        ];

        PriceList =
        [
            new(1, 1, 146, 1, "АО Нижфарм", PaymentType.Cashless, DateTime.Parse("2024-08-01")),
            new(2, 1, 198, 2,  "АО Нижфарм", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(5, 1, 110, 3, "Фармстрандарт-Лексредства", PaymentType.Cash, DateTime.Parse("2024-07-24")),
            new(1, 1, 146, 2, "АО Нижфарм", PaymentType.Cashless, DateTime.Parse("2024-08-01")),
            new(2, 1, 198, 3, "АО Нижфарм", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(5, 1, 110, 1, "Фармстрандарт-Лексредства", PaymentType.Cash, DateTime.Parse("2024-07-24")),

            new(3, 2, 99, 1, "ООО Тульская Фармацевтическая Фабрика", PaymentType.Cashless, DateTime.Parse("2024-09-05")),
            new(4, 2, 211, 2, "ЛТД Рекитт Бенкизер Хелскэр Интернешнл", PaymentType.Cashless, DateTime.Parse("2024-08-11")),
            new(3, 2, 99, 3, "ООО Тульская Фармацевтическая Фабрика", PaymentType.Cashless, DateTime.Parse("2024-09-05")),
            new(4, 2, 211, 2, "ЛТД Рекитт Бенкизер Хелскэр Интернешнл", PaymentType.Cashless, DateTime.Parse("2024-08-11")),

            new(2, 3, 190, 3, "АО Нижфарм", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(6, 3, 150, 4, "ООО Лекарь", PaymentType.Cashless, DateTime.Parse("2024-09-01")),
            new(8, 3,382, 2, "ЗАО Лекко", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new(6, 3, 150, 3, "ООО Лекарь", PaymentType.Cashless, DateTime.Parse("2024-09-01")),
            new(8, 3, 382, 1, "ЗАО Лекко", PaymentType.Cash, DateTime.Parse("2024-08-30")),

            new(2, 4, 190, 4, "АО Нижфарм", PaymentType.Cash, DateTime.Parse("2024-09-12")),
            new(2, 4, 190, 3, "АО Нижфарм", PaymentType.Cashless, DateTime.Parse("2024-09-14")),
            new(9, 4, 144, 4, "Jelfa S A", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new(9, 4, 144, 5, "Jelfa S A", PaymentType.Cash, DateTime.Parse("2024-08-30")),
            new(7, 4, 139, 1, "ЗАО Лекко", PaymentType.Cash, DateTime.Parse("2024-09-03")),
            new(7, 4, 139, 1, "ЗАО Лекко", PaymentType.Cash, DateTime.Parse("2024-09-03")),
        ];

        PharmaceuticalGroups =
        [
            new(1, "Антикоагулянт"),
            new(2, "Антибактериальное средство"),
            new(2, "Дезинфицирующее средство"),
            new(3, "Линимент"),
            new(4, "Нестероидное противовоспалительное средство"),
            new(5, "Сосудосуживающий препарат"),
            new(6, "Антибактериальное средство"),
            new(7, "Антибактериальное средство"),
            new(8, "Анальгезирующее средство"),
            new(8, "Нестероидное противовоспалительное средство"),
            new(8, "Психостимулирующее средство"),
            new(9, "Нестероидное противовоспалительное средство"),
        ];

        ProductList =
        [
            new(1, "Гепариновая мазь", "Мазь для наружного применения"),
            new(2, "Левомеколь", "Мазь для наружного применения"),
            new(3, "Линимент бальзамический (по Вишневскому)", "Мазь для наружного применения"),
            new(4, "Нурофен таблетки", "Таблетки, покрытые оболочкой"),
            new(5, "Риностоп спрей", "Спрей назальный"),
            new(6, "Стрептоцид порошок", "Порошок для наружного применения"),
            new(7, "Сульфацил-натрия капли глазные", "Капли глазные"),
            new(8, "Пенталгин Экстра гель", "Гель для наружного применения"),
            new(9, "Тромбо АСС таблетки", "Таблетки, покрытые оболочкой"),
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

using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Data;

/// <summary>
/// Контекст базы данных PostgreSQL для службы доставки еды.
/// </summary>
public class FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    public DbSet<DishCategory> Categories => Set<DishCategory>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<Order> Orders => Set<Order>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(30);
            entity.Property(e => e.DeliveryAddress).IsRequired().HasMaxLength(300);

            entity.HasData(
                new Client { Id = 1, FullName = "Зубенко Михаил Петрович", Phone = "+79277777777", DeliveryAddress = "г. Самара, ул. Ленина, д. 10" },
                new Client { Id = 2, FullName = "Дылда Ольга Олеговна", Phone = "+79270001111", DeliveryAddress = "г. Самара, ул. Гагарина, д. 15" },
                new Client { Id = 3, FullName = "Заболотный Антон Иванович", Phone = "+79273333333", DeliveryAddress = "г. Самара, ул. Гаражная, д. 21" },
                new Client { Id = 4, FullName = "Скорлупа Иннокентий Сергеевич", Phone = "+79270101010", DeliveryAddress = "г. Самара, ул. Гая, д. 8" },
                new Client { Id = 5, FullName = "Иванов Иван Иванович", Phone = "+79270202020", DeliveryAddress = "г. Самара, ул. Революционная, д. 32" },
                new Client { Id = 6, FullName = "Петушков Илья Викторович", Phone = "+7270000001", DeliveryAddress = "г. Самара, ул. Ново-Садовая, д. 14" },
                new Client { Id = 7, FullName = "Костыль Ирина Петровна", Phone = "+79371004554", DeliveryAddress = "г. Самара, ул. Стара-Загора, д. 5" },
                new Client { Id = 8, FullName = "Петров Петр Петрович", Phone = "+7271232323", DeliveryAddress = "г. Самара, ул. Дыбенко, д. 19" },
                new Client { Id = 9, FullName = "Плотников Иван Иванович", Phone = "+79276547656", DeliveryAddress = "г. Самара, ул. Гаражная, д. 27" },
                new Client { Id = 10, FullName = "Колодин Алексей Сергеевич", Phone = "+7271044141", DeliveryAddress = "г. Самара, ул. Мяги, д. 11" }
            );
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Address).IsRequired().HasMaxLength(300);

            entity.HasData(
                new Restaurant { Id = 1, Name = "Ростикс", Address = "г. Самара, ул. Аэродромная, д. 25", Rating = 4.8, OpenTime = new TimeOnly(9, 0), CloseTime = new TimeOnly(23, 0) },
                new Restaurant { Id = 2, Name = "Вкусно и точка", Address = "г. Самара, ул. Дыбенко, д. 30", Rating = 4.5, OpenTime = new TimeOnly(8, 0), CloseTime = new TimeOnly(23, 0) },
                new Restaurant { Id = 3, Name = "Фуджи", Address = "г. Самара, ул. Гагарина, д. 42", Rating = 4.6, OpenTime = new TimeOnly(10, 0), CloseTime = new TimeOnly(23, 0) },
                new Restaurant { Id = 4, Name = "Тануки", Address = "г. Самара, ул. Стара-Загора, д. 35", Rating = 4.8, OpenTime = new TimeOnly(11, 0), CloseTime = new TimeOnly(23, 0) },
                new Restaurant { Id = 5, Name = "Комитет", Address = "г. Самара, ул. Дыбенко, д. 12", Rating = 4.9, OpenTime = new TimeOnly(10, 0), CloseTime = new TimeOnly(23, 15) },
                new Restaurant { Id = 6, Name = "Шашлычная", Address = "г. Самара, ул. Революционная, д. 51", Rating = 4.3, OpenTime = new TimeOnly(9, 0), CloseTime = new TimeOnly(22, 30) },
                new Restaurant { Id = 7, Name = "Перчини", Address = "г. Самара, ул. Ново-Садовая, д. 76", Rating = 4.6, OpenTime = new TimeOnly(8, 30), CloseTime = new TimeOnly(22, 0) },
                new Restaurant { Id = 8, Name = "Дом Нино", Address = "г. Самара, ул. Пушкина, д. 14", Rating = 4.9, OpenTime = new TimeOnly(11, 0), CloseTime = new TimeOnly(23, 0) },
                new Restaurant { Id = 9, Name = "Супра", Address = "г. Самара, ул. Мяги, д. 28", Rating = 4.9, OpenTime = new TimeOnly(10, 0), CloseTime = new TimeOnly(23, 0) },
                new Restaurant { Id = 10, Name = "Кафе Чаша", Address = "г. Самара, ул. Куйбышева, д. 10", Rating = 4.5, OpenTime = new TimeOnly(8, 0), CloseTime = new TimeOnly(18, 0) }
            );
        });

        modelBuilder.Entity<DishCategory>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

            entity.HasData(
                new DishCategory { Id = 1, Name = "Пицца" },
                new DishCategory { Id = 2, Name = "Суши" },
                new DishCategory { Id = 3, Name = "Бургеры" },
                new DishCategory { Id = 4, Name = "Салаты" },
                new DishCategory { Id = 5, Name = "Супы" },
                new DishCategory { Id = 6, Name = "Горячие блюда" },
                new DishCategory { Id = 7, Name = "Десерты" },
                new DishCategory { Id = 8, Name = "Напитки" },
                new DishCategory { Id = 9, Name = "Закуски" },
                new DishCategory { Id = 10, Name = "Соусы" }
            );
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Price).HasPrecision(10, 2);

            entity.HasOne(d => d.Category)
                .WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Restaurant)
                .WithMany()
                .HasForeignKey(d => d.RestaurantId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(
                new Dish { Id = 1, Name = "Пицца Маргарита", Weight = 450, Price = 550m, CategoryId = 1, RestaurantId = 1 },
                new Dish { Id = 2, Name = "Чикенбургер", Weight = 300, Price = 420m, CategoryId = 3, RestaurantId = 1 },
                new Dish { Id = 3, Name = "Бургер классический", Weight = 320, Price = 390m, CategoryId = 3, RestaurantId = 2 },
                new Dish { Id = 4, Name = "Картофель фри", Weight = 180, Price = 190m, CategoryId = 9, RestaurantId = 2 },
                new Dish { Id = 5, Name = "Филадельфия", Weight = 280, Price = 650m, CategoryId = 2, RestaurantId = 3 },
                new Dish { Id = 6, Name = "Калифорния", Weight = 260, Price = 590m, CategoryId = 2, RestaurantId = 3 },
                new Dish { Id = 7, Name = "Том Ям", Weight = 400, Price = 720m, CategoryId = 5, RestaurantId = 4 },
                new Dish { Id = 8, Name = "Лапша с курицей", Weight = 350, Price = 540m, CategoryId = 6, RestaurantId = 4 },
                new Dish { Id = 9, Name = "Стейк из говядины", Weight = 350, Price = 1250m, CategoryId = 6, RestaurantId = 5 },
                new Dish { Id = 10, Name = "Салат Цезарь", Weight = 250, Price = 490m, CategoryId = 4, RestaurantId = 5 },
                new Dish { Id = 11, Name = "Шашлык из свинины", Weight = 400, Price = 850m, CategoryId = 6, RestaurantId = 6 },
                new Dish { Id = 12, Name = "Паста Карбонара", Weight = 350, Price = 620m, CategoryId = 6, RestaurantId = 7 },
                new Dish { Id = 13, Name = "Тирамису", Weight = 180, Price = 390m, CategoryId = 7, RestaurantId = 8 },
                new Dish { Id = 14, Name = "Хачапури", Weight = 400, Price = 580m, CategoryId = 6, RestaurantId = 9 },
                new Dish { Id = 15, Name = "Борщ", Weight = 350, Price = 360m, CategoryId = 5, RestaurantId = 10 }
            );
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TotalPrice).HasPrecision(10, 2);

            entity.HasOne(o => o.Client)
                .WithMany()
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Restaurant)
                .WithMany()
                .HasForeignKey(o => o.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(
                new Order { Id = 1, ClientId = 1, RestaurantId = 1, OrderDateTime = new DateTime(2026, 9, 1, 12, 10, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 1, 12, 45, 0, DateTimeKind.Utc), TotalPrice = 970m },
                new Order { Id = 2, ClientId = 2, RestaurantId = 2, OrderDateTime = new DateTime(2026, 9, 1, 13, 20, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 1, 13, 55, 0, DateTimeKind.Utc), TotalPrice = 580m },
                new Order { Id = 3, ClientId = 3, RestaurantId = 3, OrderDateTime = new DateTime(2026, 9, 1, 14, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 1, 14, 40, 0, DateTimeKind.Utc), TotalPrice = 650m },
                new Order { Id = 4, ClientId = 4, RestaurantId = 4, OrderDateTime = new DateTime(2026, 9, 1, 15, 15, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 1, 15, 50, 0, DateTimeKind.Utc), TotalPrice = 1260m },
                new Order { Id = 5, ClientId = 5, RestaurantId = 5, OrderDateTime = new DateTime(2026, 9, 2, 12, 30, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 2, 13, 20, 0, DateTimeKind.Utc), TotalPrice = 1740m },
                new Order { Id = 6, ClientId = 6, RestaurantId = 6, OrderDateTime = new DateTime(2026, 9, 2, 13, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 2, 13, 45, 0, DateTimeKind.Utc), TotalPrice = 850m },
                new Order { Id = 7, ClientId = 7, RestaurantId = 7, OrderDateTime = new DateTime(2026, 9, 2, 14, 10, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 2, 14, 50, 0, DateTimeKind.Utc), TotalPrice = 620m },
                new Order { Id = 8, ClientId = 8, RestaurantId = 8, OrderDateTime = new DateTime(2026, 9, 2, 15, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 2, 15, 35, 0, DateTimeKind.Utc), TotalPrice = 390m },
                new Order { Id = 9, ClientId = 9, RestaurantId = 9, OrderDateTime = new DateTime(2026, 9, 3, 11, 30, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 3, 12, 10, 0, DateTimeKind.Utc), TotalPrice = 580m },
                new Order { Id = 10, ClientId = 10, RestaurantId = 10, OrderDateTime = new DateTime(2026, 9, 3, 12, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 3, 12, 30, 0, DateTimeKind.Utc), TotalPrice = 360m },
                new Order { Id = 11, ClientId = 1, RestaurantId = 1, OrderDateTime = new DateTime(2026, 9, 3, 13, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 3, 13, 25, 0, DateTimeKind.Utc), TotalPrice = 550m },
                new Order { Id = 12, ClientId = 2, RestaurantId = 2, OrderDateTime = new DateTime(2026, 9, 3, 14, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 3, 14, 30, 0, DateTimeKind.Utc), TotalPrice = 390m },
                new Order { Id = 13, ClientId = 3, RestaurantId = 3, OrderDateTime = new DateTime(2026, 9, 4, 12, 15, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 4, 12, 50, 0, DateTimeKind.Utc), TotalPrice = 1240m },
                new Order { Id = 14, ClientId = 4, RestaurantId = 4, OrderDateTime = new DateTime(2026, 9, 4, 13, 30, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 4, 14, 20, 0, DateTimeKind.Utc), TotalPrice = 720m },
                new Order { Id = 15, ClientId = 5, RestaurantId = 5, OrderDateTime = new DateTime(2026, 9, 4, 15, 0, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 4, 15, 45, 0, DateTimeKind.Utc), TotalPrice = 1250m },
                new Order { Id = 16, ClientId = 6, RestaurantId = 1, OrderDateTime = new DateTime(2026, 9, 5, 12, 30, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 5, 13, 5, 0, DateTimeKind.Utc), TotalPrice = 970m },
                new Order { Id = 17, ClientId = 7, RestaurantId = 2, OrderDateTime = new DateTime(2026, 9, 5, 13, 15, 0, DateTimeKind.Utc), DeliveryDateTime = new DateTime(2026, 9, 5, 13, 50, 0, DateTimeKind.Utc), TotalPrice = 580m }
            );

            // Связь многие-ко-многим Order <-> Dish с начальным заполнением связующей таблицы
            entity.HasMany(o => o.Dishes)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "OrderDishes",
                    j => j.HasOne<Dish>().WithMany().HasForeignKey("DishId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Order>().WithMany().HasForeignKey("OrderId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("OrderId", "DishId");
                        j.HasData(
                            new { OrderId = 1, DishId = 1 },
                            new { OrderId = 1, DishId = 2 },
                            new { OrderId = 2, DishId = 3 },
                            new { OrderId = 2, DishId = 4 },
                            new { OrderId = 3, DishId = 5 },
                            new { OrderId = 4, DishId = 7 },
                            new { OrderId = 4, DishId = 8 },
                            new { OrderId = 5, DishId = 9 },
                            new { OrderId = 5, DishId = 10 },
                            new { OrderId = 6, DishId = 11 },
                            new { OrderId = 7, DishId = 12 },
                            new { OrderId = 8, DishId = 13 },
                            new { OrderId = 9, DishId = 14 },
                            new { OrderId = 10, DishId = 15 },
                            new { OrderId = 11, DishId = 1 },
                            new { OrderId = 12, DishId = 3 },
                            new { OrderId = 13, DishId = 5 },
                            new { OrderId = 13, DishId = 6 },
                            new { OrderId = 14, DishId = 7 },
                            new { OrderId = 15, DishId = 9 },
                            new { OrderId = 16, DishId = 1 },
                            new { OrderId = 16, DishId = 2 },
                            new { OrderId = 17, DishId = 3 },
                            new { OrderId = 17, DishId = 4 }
                        );
                    });
        });
    }
}
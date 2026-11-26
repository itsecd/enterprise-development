using Bogus;
using DryCleaning.Contracts.Dto;

namespace DryCleaning.Generator.Generation;

/// <summary>
/// Генератор случайных заказов для отправки на сервер
/// </summary>
public static class OrderGenerator
{
    /// <summary>
    /// Правила заполнения заказа. В начальных данных сервера клиенты 1-10 и изделия 1-20.
    /// Диапазоны взяты чуть шире, чтобы часть заказов ссылалась на несуществующие записи
    /// и сервер отклонял их при проверке
    /// </summary>
    private static readonly Faker<OrderCreateDto> _faker = new Faker<OrderCreateDto>()
        .RuleFor(order => order.ClientId, faker => faker.Random.Int(1, 12))
        .RuleFor(order => order.ItemId, faker => faker.Random.Int(1, 22))
        .RuleFor(order => order.ProcessingDays, faker => faker.Random.Int(1, 14));

    /// <summary>
    /// Создаёт заданное количество случайных заказов
    /// </summary>
    /// <param name="count">Количество заказов</param>
    /// <returns>Список сгенерированных заказов</returns>
    public static IList<OrderCreateDto> Generate(int count) => _faker.Generate(count);
}

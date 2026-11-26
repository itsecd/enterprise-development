using Bogus;
using DryCleaning.Contracts.Dto;

namespace DryCleaning.Generator.Generation;

/// <summary>
/// Генератор случайных заказов для отправки на сервер
/// </summary>
public static class OrderGenerator
{
    /// <summary>
    /// Правила заполнения заказа. Идентификаторы клиентов и изделий
    /// берутся из диапазонов начальных данных сервера
    /// </summary>
    private static readonly Faker<OrderCreateDto> _faker = new Faker<OrderCreateDto>()
        .RuleFor(order => order.ClientId, faker => faker.Random.Int(1, 10))
        .RuleFor(order => order.ItemId, faker => faker.Random.Int(1, 20))
        .RuleFor(order => order.ProcessingDays, faker => faker.Random.Int(1, 14));

    /// <summary>
    /// Создаёт заданное количество случайных заказов
    /// </summary>
    /// <param name="count">Количество заказов</param>
    /// <returns>Список сгенерированных заказов</returns>
    public static IList<OrderCreateDto> Generate(int count) => _faker.Generate(count);
}

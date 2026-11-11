using DryCleaner.Application.Contracts.Analytic;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Преобразует результаты аналитических запросов в соответствующие DTO.
/// </summary>
internal class AnalyticMapper
{
    /// <summary>
    /// Преобразует клиента и количество его заказов в объект ClientOrdersCountDto.
    /// </summary>
    /// <param name="client">Клиент.</param>
    /// <param name="ordersCount">Количество заказов клиента.</param>
    /// <returns>Экземпляр ClientOrdersCountDto.</returns>
    public static ClientOrdersCountDto Map(Client client, int ordersCount)
    {
        return new(ClientMapper.Map(client), ordersCount);
    }

    /// <summary>
    /// Преобразует клиента и средний срок выполнения его заказов в объект ClientAverageProcessingDaysDto.
    /// </summary>
    /// <param name="client">Клиент.</param>
    /// <param name="averageProcessingDays">Средний срок выполнения заказов в днях.</param>
    /// <returns>Экземпляр ClientAverageProcessingDaysDto.</returns>
    public static ClientAverageProcessingDaysDto Map(Client client, double averageProcessingDays)
    {
        return new(ClientMapper.Map(client), averageProcessingDays);
    }

    /// <summary>
    /// Преобразует категорию изделия и количество заказов в объект CategoryOrdersCountDto.
    /// </summary>
    /// <param name="category">Категория изделия.</param>
    /// <param name="ordersCount">Количество заказов изделий этой категории.</param>
    /// <returns>Экземпляр CategoryOrdersCountDto.</returns>
    public static CategoryOrdersCountDto Map(ProductCategory category, int ordersCount)
    {
        return new(ProductCategoryMapper.Map(category), ordersCount);
    }

    /// <summary>
    /// Преобразует клиента и потраченную им сумму в объект ClientTotalSpentDto.
    /// </summary>
    /// <param name="client">Клиент.</param>
    /// <param name="totalSpent">Потраченная сумма.</param>
    /// <returns>Экземпляр ClientTotalSpentDto.</returns>
    public static ClientTotalSpentDto Map(Client client, decimal totalSpent)
    {
        return new(ClientMapper.Map(client), totalSpent);
    }
}

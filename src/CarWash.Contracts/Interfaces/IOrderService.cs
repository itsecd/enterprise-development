using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с заказами
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Возвращает все заказы
    /// </summary>
    /// <returns>Список заказов</returns>
    public Task<IEnumerable<OrderDto>> GetAllAsync();

    /// <summary>
    /// Возвращает заказ по ID
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <returns>Данные заказа или null, если не найдено</returns>
    public Task<OrderDto?> GetByIdAsync(int id);

    /// <summary>
    /// Создаёт заказ
    /// </summary>
    /// <param name="editDto">Данные заказа</param>
    /// <returns>Созданный заказ</returns>
    /// <exception cref="KeyNotFoundException">Клиент, автомобиль или услуга не найдены</exception>
    public Task<OrderDto> CreateAsync(OrderEditDto editDto);

    /// <summary>
    /// Изменяет заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <param name="editDto">Новые данные заказа</param>
    /// <exception cref="KeyNotFoundException">Заказ, клиент, автомобиль или услуга не найдены</exception>
    public Task UpdateAsync(int id, OrderEditDto editDto);

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <exception cref="KeyNotFoundException">Объект не найден</exception>
    public Task DeleteAsync(int id);
}

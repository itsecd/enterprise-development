namespace FoodDelivery.Domain.Repositories;

/// <summary>Выполняет проверку и запись связанных данных заказа как одну операцию</summary>
public interface IOrderWriteTransaction
{
    /// <summary>Выполняет операцию записи</summary>
    /// <typeparam name="T">Тип результата</typeparam>
    /// <param name="operation">Проверка и изменение данных</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Результат операции</returns>
    Task<T> ExecuteAsync<T>(Func<CancellationToken, Task<T>> operation, CancellationToken cancellationToken);
}

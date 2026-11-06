namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Определяет границу согласованной операции с хранилищем
/// </summary>
public interface IDataSession
{
    /// <summary>
    /// Выполняет операцию с результатом в согласованном состоянии хранилища
    /// </summary>
    /// <typeparam name="T">Тип результата</typeparam>
    /// <param name="operation">Операция</param>
    /// <returns>Результат операции</returns>
    T Execute<T>(Func<T> operation);

    /// <summary>
    /// Выполняет согласованную операцию изменения хранилища
    /// </summary>
    /// <param name="operation">Операция</param>
    void Execute(Action operation);
}

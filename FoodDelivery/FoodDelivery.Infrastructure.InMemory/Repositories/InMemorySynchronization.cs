using System.Runtime.CompilerServices;
using FoodDelivery.Domain.Data;

namespace FoodDelivery.Infrastructure.InMemory.Repositories;

/// <summary>
/// Согласует доступ к одному графу данных в памяти
/// </summary>
internal static class InMemorySynchronization
{
    private static readonly ConditionalWeakTable<FoodDeliveryData, Lock> Locks = new();

    /// <summary>
    /// Возвращает блокировку заданного экземпляра данных
    /// </summary>
    /// <param name="data">Данные приложения</param>
    /// <returns>Объект синхронизации</returns>
    internal static Lock For(FoodDeliveryData data)
    {
        return Locks.GetValue(data, _ => new Lock());
    }
}

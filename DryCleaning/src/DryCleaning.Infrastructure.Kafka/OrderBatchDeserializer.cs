using System.Text.Json;
using Confluent.Kafka;
using DryCleaning.Contracts.Dto;

namespace DryCleaning.Infrastructure.Kafka;

/// <summary>
/// Превращает JSON из сообщения Kafka обратно в пачку заказов
/// </summary>
public class OrderBatchDeserializer : IDeserializer<IList<OrderCreateDto>>
{
    /// <summary>
    /// Читает пачку заказов из массива байт с текстом JSON
    /// </summary>
    /// <param name="data">Содержимое сообщения</param>
    /// <param name="isNull">Признак того, что сообщение пришло без содержимого</param>
    /// <param name="context">Сведения о сообщении и топике, здесь не используются</param>
    /// <returns>Пачка заказов, пустая для сообщения без содержимого</returns>
    public IList<OrderCreateDto> Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) =>
        isNull ? [] : JsonSerializer.Deserialize<IList<OrderCreateDto>>(data) ?? [];
}

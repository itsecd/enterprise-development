using System.Text.Json;
using Confluent.Kafka;
using DryCleaning.Contracts.Dto;

namespace DryCleaning.Generator.Messaging;

/// <summary>
/// Превращает пачку заказов в JSON для записи в сообщение Kafka
/// </summary>
public class OrderBatchSerializer : ISerializer<IList<OrderCreateDto>>
{
    /// <summary>
    /// Записывает пачку заказов в виде массива байт с текстом JSON
    /// </summary>
    /// <param name="data">Пачка заказов</param>
    /// <param name="context">Сведения о сообщении и топике, здесь не используются</param>
    /// <returns>Массив байт для значения сообщения</returns>
    public byte[] Serialize(IList<OrderCreateDto> data, SerializationContext context) =>
        JsonSerializer.SerializeToUtf8Bytes(data);
}

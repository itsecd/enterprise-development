using Confluent.Kafka;
using DryCleaning.Contracts.Dto;

namespace DryCleaning.Generator.Messaging;

/// <summary>
/// Отправляет пачки заказов в топик Kafka.
/// Одна пачка уходит одним сообщением
/// </summary>
/// <param name="configuration">Конфигурация приложения</param>
/// <param name="producer">Продюсер Kafka</param>
/// <param name="logger">Логгер</param>
public class KafkaOrderProducer(
    IConfiguration configuration,
    IProducer<Null, IList<OrderCreateDto>> producer,
    ILogger<KafkaOrderProducer> logger) : IOrderProducer
{
    private readonly string _topicName = configuration["Kafka:TopicName"]
        ?? throw new KeyNotFoundException("В конфигурации не задано имя топика Kafka:TopicName");

    /// <summary>
    /// Отправляет пачку заказов в топик и записывает в лог, куда она попала
    /// </summary>
    /// <param name="batch">Пачка заказов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task SendAsync(IList<OrderCreateDto> batch, CancellationToken cancellationToken)
    {
        var message = new Message<Null, IList<OrderCreateDto>> { Value = batch };

        try
        {
            var result = await producer.ProduceAsync(_topicName, message, cancellationToken);

            logger.LogInformation("Пачка из {Count} заказов отправлена в топик {Topic} (партиция {Partition}, смещение {Offset})",
                batch.Count, _topicName, result.Partition.Value, result.Offset.Value);
        }
        catch (ProduceException<Null, IList<OrderCreateDto>> exception)
        {
            logger.LogError(exception, "Не удалось отправить пачку из {Count} заказов в топик {Topic}: {Reason}",
                batch.Count, _topicName, exception.Error.Reason);
            throw;
        }
    }
}

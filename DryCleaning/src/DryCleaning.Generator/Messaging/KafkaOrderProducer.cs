using Confluent.Kafka;
using DryCleaning.Contracts.Dto;
using DryCleaning.Generator.Options;
using Microsoft.Extensions.Options;

namespace DryCleaning.Generator.Messaging;

/// <summary>
/// Отправляет пачки заказов в топик Kafka.
/// Одна пачка уходит одним сообщением. Если брокер недоступен,
/// отправка повторяется несколько раз с растущей паузой
/// </summary>
/// <param name="options">Настройки отправки в Kafka</param>
/// <param name="producer">Продюсер Kafka</param>
/// <param name="logger">Логгер</param>
public class KafkaOrderProducer(
    IOptions<KafkaOptions> options,
    IProducer<Null, IList<OrderCreateDto>> producer,
    ILogger<KafkaOrderProducer> logger) : IOrderProducer
{
    private readonly KafkaOptions _options = options.Value;

    /// <summary>
    /// Отправляет пачку заказов в топик и записывает в лог, куда она попала.
    /// При ошибке ждёт и пробует снова, пока не закончатся попытки
    /// </summary>
    /// <param name="batch">Пачка заказов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="ProduceException{TKey, TValue}">Пачку не удалось отправить за все попытки</exception>
    public async Task SendAsync(IList<OrderCreateDto> batch, CancellationToken cancellationToken)
    {
        var message = new Message<Null, IList<OrderCreateDto>> { Value = batch };
        var delay = TimeSpan.FromSeconds(_options.RetryDelay);

        for (var attempt = 1; ; attempt++)
        {
            try
            {
                var result = await producer.ProduceAsync(_options.TopicName, message, cancellationToken);

                logger.LogInformation("Пачка из {Count} заказов отправлена в топик {Topic} (партиция {Partition}, смещение {Offset})",
                    batch.Count, _options.TopicName, result.Partition.Value, result.Offset.Value);
                return;
            }
            catch (ProduceException<Null, IList<OrderCreateDto>> exception) when (attempt < _options.MaxAttempts)
            {
                logger.LogWarning("Не удалось отправить пачку в топик {Topic}: {Reason}. Попытка {Attempt} из {MaxAttempts}, следующая через {Delay} с",
                    _options.TopicName, exception.Error.Reason, attempt, _options.MaxAttempts, delay.TotalSeconds);

                await Task.Delay(delay, cancellationToken);
                delay *= 2;
            }
            catch (ProduceException<Null, IList<OrderCreateDto>> exception)
            {
                logger.LogError(exception, "Пачка из {Count} заказов не отправлена в топик {Topic} за {MaxAttempts} попыток: {Reason}",
                    batch.Count, _options.TopicName, _options.MaxAttempts, exception.Error.Reason);
                throw;
            }
        }
    }
}

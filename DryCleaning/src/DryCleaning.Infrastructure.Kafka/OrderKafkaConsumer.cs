using System.ComponentModel.DataAnnotations;
using Confluent.Kafka;
using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DryCleaning.Infrastructure.Kafka;

/// <summary>
/// Фоновая служба: читает пачки заказов из топика Kafka
/// и передаёт каждый заказ в сервис заказов для сохранения.
/// Ошибки Kafka не останавливают приложение: API продолжает отвечать на запросы
/// </summary>
/// <param name="configuration">Конфигурация приложения</param>
/// <param name="consumer">Потребитель Kafka</param>
/// <param name="scopeFactory">Фабрика областей для получения сервиса заказов</param>
/// <param name="logger">Логгер</param>
public class OrderKafkaConsumer(
    IConfiguration configuration,
    IConsumer<Null, IList<OrderCreateDto>> consumer,
    IServiceScopeFactory scopeFactory,
    ILogger<OrderKafkaConsumer> logger) : BackgroundService
{
    private readonly string _topicName = configuration["Kafka:TopicName"]
        ?? throw new KeyNotFoundException("В конфигурации не задано имя топика Kafka:TopicName");

    /// <summary>
    /// Пауза перед повторной попыткой чтения после ошибки
    /// </summary>
    private static readonly TimeSpan _retryDelay = TimeSpan.FromSeconds(2);

    /// <summary>
    /// Цикл работы службы: дождаться сообщения, сохранить заказы из него, ждать следующее
    /// </summary>
    /// <param name="stoppingToken">Токен остановки приложения</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Ожидание сообщения блокирует поток, поэтому сразу освобождаем поток запуска приложения
        await Task.Yield();

        try
        {
            consumer.Subscribe(_topicName);
            logger.LogInformation("Потребитель подписан на топик {Topic}", _topicName);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var result = consumer.Consume(stoppingToken);
                    if (result?.Message?.Value is null)
                        continue;

                    logger.LogInformation("Получена пачка из {Count} заказов из топика {Topic} (партиция {Partition}, смещение {Offset})",
                        result.Message.Value.Count, result.Topic, result.Partition.Value, result.Offset.Value);

                    await SaveBatchAsync(result.Message.Value, stoppingToken);
                }
                catch (ConsumeException exception) when (!exception.Error.IsFatal)
                {
                    logger.LogWarning("Не удалось прочитать сообщение из топика {Topic}: {Reason}. Повтор через {Delay} с",
                        _topicName, exception.Error.Reason, _retryDelay.TotalSeconds);

                    await Task.Delay(_retryDelay, stoppingToken);
                }
            }
        }
        catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Чтение из топика {Topic} остановлено", _topicName);
        }
        catch (Exception exception)
        {
            // Необработанное исключение фоновой службы остановило бы всё приложение,
            // поэтому прекращаем только чтение из Kafka
            logger.LogError(exception, "Чтение из топика {Topic} прекращено из-за ошибки, заказы из Kafka больше не принимаются", _topicName);
        }
        finally
        {
            consumer.Close();
        }
    }

    /// <summary>
    /// Сохраняет заказы из пачки по одному.
    /// Заказ, не прошедший проверку, пропускается с записью в лог и не мешает остальным
    /// </summary>
    /// <param name="batch">Пачка заказов</param>
    /// <param name="cancellationToken">Токен отмены</param>
    private async Task SaveBatchAsync(IList<OrderCreateDto> batch, CancellationToken cancellationToken)
    {
        // Сервис заказов и контекст базы живут в пределах области, поэтому на каждую пачку создаём свою
        await using var scope = scopeFactory.CreateAsyncScope();
        var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

        var saved = 0;
        var skipped = 0;

        try
        {
            foreach (var order in batch)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    await orderService.AddAsync(order);
                    saved++;
                }
                catch (ValidationException exception)
                {
                    logger.LogWarning("Заказ пропущен: {Reason}. Клиент {ClientId}, изделие {ItemId}, срок {ProcessingDays}",
                        exception.Message, order.ClientId, order.ItemId, order.ProcessingDays);
                    skipped++;
                }
            }

            logger.LogInformation("Пачка обработана: сохранено {Saved}, пропущено {Skipped}", saved, skipped);
        }
        catch (Exception exception) when (exception is not OperationCanceledException)
        {
            logger.LogError(exception, "Ошибка при сохранении пачки: сохранено {Saved} из {Count} заказов", saved, batch.Count);
        }
    }
}

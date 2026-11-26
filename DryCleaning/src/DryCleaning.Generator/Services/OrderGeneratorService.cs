using DryCleaning.Generator.Generation;
using DryCleaning.Generator.Messaging;
using DryCleaning.Generator.Options;
using Microsoft.Extensions.Options;

namespace DryCleaning.Generator.Services;

/// <summary>
/// Фоновая служба: генерирует заказы пачками и отправляет их на сервер
/// с заданной паузой, пока не будет отправлено заданное общее количество
/// </summary>
/// <param name="options">Настройки генератора</param>
/// <param name="producer">Отправитель заказов</param>
/// <param name="logger">Логгер</param>
public class OrderGeneratorService(
    IOptions<GeneratorOptions> options,
    IOrderProducer producer,
    ILogger<OrderGeneratorService> logger) : BackgroundService
{
    /// <summary>
    /// Цикл работы службы: сгенерировать пачку, отправить, подождать
    /// </summary>
    /// <param name="stoppingToken">Токен остановки приложения</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var settings = options.Value;

        logger.LogInformation("Генератор запущен: всего {Total} заказов, по {Batch} в пачке, пауза {Wait} с",
            settings.PayloadLimit, settings.BatchSize, settings.WaitTime);

        var sent = 0;
        while (sent < settings.PayloadLimit && !stoppingToken.IsCancellationRequested)
        {
            var batch = OrderGenerator.Generate(Math.Min(settings.BatchSize, settings.PayloadLimit - sent));

            await producer.SendAsync(batch, stoppingToken);
            sent += batch.Count;

            // После последней пачки ждать уже нечего
            if (sent < settings.PayloadLimit)
                await Task.Delay(TimeSpan.FromSeconds(settings.WaitTime), stoppingToken);
        }

        logger.LogInformation("Генератор завершил работу: отправлено {Sent} заказов", sent);
    }
}

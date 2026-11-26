using System.ComponentModel.DataAnnotations;

namespace DryCleaning.Generator.Options;

/// <summary>
/// Настройки отправки заказов в Kafka
/// </summary>
public class KafkaOptions
{
    /// <summary>
    /// Имя раздела конфигурации с настройками Kafka
    /// </summary>
    public const string SectionName = "Kafka";

    /// <summary>
    /// Имя топика, в который отправляются заказы
    /// </summary>
    [Required(ErrorMessage = "Не задано имя топика")]
    public string TopicName { get; set; } = string.Empty;

    /// <summary>
    /// Сколько раз генератор пробует отправить одну пачку, прежде чем сдаться
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Количество попыток должно быть больше нуля")]
    public int MaxAttempts { get; set; }

    /// <summary>
    /// Пауза перед первой повторной попыткой в секундах.
    /// После каждой неудачи пауза удваивается
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Пауза между попытками должна быть больше нуля")]
    public int RetryDelay { get; set; }
}

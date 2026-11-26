using System.ComponentModel.DataAnnotations;

namespace DryCleaning.Generator.Options;

/// <summary>
/// Настройки генератора заказов
/// </summary>
public class GeneratorOptions
{
    /// <summary>
    /// Имя раздела конфигурации с настройками генератора
    /// </summary>
    public const string SectionName = "Generator";

    /// <summary>
    /// Количество заказов в одной пачке
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Размер пачки должен быть больше нуля")]
    public int BatchSize { get; set; }

    /// <summary>
    /// Общее количество заказов, после отправки которых генератор останавливается
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Общее количество заказов должно быть больше нуля")]
    public int PayloadLimit { get; set; }

    /// <summary>
    /// Пауза между отправками пачек в секундах
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "Пауза между отправками должна быть больше нуля")]
    public int WaitTime { get; set; }
}

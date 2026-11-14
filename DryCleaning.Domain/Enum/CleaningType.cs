namespace DryCleaning.Domain.Enum;

/// <summary>
/// Вид чистки изделия
/// </summary>
public enum CleaningType
{
    /// <summary>
    /// Химчистка
    /// </summary>
    DryCleaning = 0,

    /// <summary>
    /// Аквачистка
    /// </summary>
    WetCleaning = 1,

    /// <summary>
    /// Паровая обработка
    /// </summary>
    SteamCleaning = 2,

    /// <summary>
    /// Ручная стирка
    /// </summary>
    HandWash = 3,

    /// <summary>
    /// Глажение
    /// </summary>
    Ironing = 4
}
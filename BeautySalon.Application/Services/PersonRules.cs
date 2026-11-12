namespace BeautySalon.Application.Services;

/// <summary>
/// Правила для данных посетителей и специалистов
/// </summary>
internal static class PersonRules
{
    internal static void ValidateBirthDate(DateOnly date)
    {
        if (date == DateOnly.MinValue ||
            date > DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("Дата рождения указана некорректно");
        }
    }
}

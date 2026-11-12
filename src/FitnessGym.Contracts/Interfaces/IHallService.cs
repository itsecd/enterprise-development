namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис залов фитнес-клуба.
/// </summary>
public interface IHallService
{
    /// <summary>
    /// Возвращает названия залов из записей на занятия.
    /// </summary>
    public Task<IReadOnlyList<string>> GetHallsAsync();

    /// <summary>
    /// Определяет, доступен ли зал для записи в указанный момент времени.
    /// Занятия с ещё не проставленным временем окончания не учитываются.
    /// </summary>
    /// <param name="hallName">Название зала.</param>
    /// <param name="at">Момент времени проверки; по умолчанию — текущий момент.</param>
    public Task<bool> IsHallAvailableAsync(string hallName, DateTime? at = null);
}

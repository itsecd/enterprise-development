using System.ComponentModel.DataAnnotations;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Получение залов и проверка их доступности.
/// </summary>
/// <param name="sessionRepository">Репозиторий занятий.</param>
/// <param name="clock">Функция получения текущего времени.</param>
public class HallService(ITrainingSessionRepository sessionRepository, Func<DateTime>? clock = null) : IHallService
{
    private readonly Func<DateTime> _clock = clock ?? (() => DateTime.Now);

    /// <inheritdoc/>
    public Task<IReadOnlyList<string>> GetHallsAsync() => sessionRepository.GetHallsAsync();

    /// <inheritdoc/>
    public Task<bool> IsHallAvailableAsync(string hallName, DateTime? at = null)
    {
        if (string.IsNullOrWhiteSpace(hallName))
        {
            throw new ValidationException("Не указано название зала.");
        }

        if (at is { Kind: not DateTimeKind.Unspecified })
        {
            throw new ValidationException("Укажите местное время клуба без часового пояса.");
        }

        return sessionRepository.IsHallAvailableAsync(hallName,
            at ?? DateTime.SpecifyKind(_clock(), DateTimeKind.Unspecified));
    }
}

namespace FitnesClub.Api.Contracts.Specializations;

/// <summary>
/// Данные для создания или обновления специализации тренера.
/// </summary>
/// <param name="Name">Наименование специализации (например, "Силовые тренировки", "Йога", "Кроссфит").</param>
public record SpecializationCreateUpdateDto(
    string Name
);
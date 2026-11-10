namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO с количеством повторных приемов для породы
/// </summary>
/// <param name="BreedId">Идентификатор породы</param>
/// <param name="RepeatAppointmentsCount">Количество повторных приемов</param>
public sealed record RepeatAppointmentsCountDto(int BreedId, int RepeatAppointmentsCount);

using FitnessClub.Api.Dtos.Specializations;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Api.Services;

/// <summary>
/// Сервис для работы со специализациями.
/// </summary>
public class SpecializationService(IRepository<Specialization> repository)
{
    /// <summary>
    /// Получить все специализации.
    /// </summary>
    public IReadOnlyList<SpecializationDto> GetAll() => [.. repository.GetAll().Select(ToDto)];

    /// <summary>
    /// Получить специализацию по идентификатору.
    /// </summary>
    public SpecializationDto? GetById(int id)
    {
        var specialization = repository.GetById(id);

        return specialization is null
            ? null
            : ToDto(specialization);
    }

    /// <summary>
    /// Создать новую специализацию.
    /// </summary>
    public SpecializationDto Create(CreateSpecializationDto dto)
    {
        Validate(dto);

        var specialization = new Specialization
        {
            Name = dto.Name
        };

        return ToDto(repository.Add(specialization));
    }

    /// <summary>
    /// Обновить специализацию.
    /// </summary>
    public bool Update(int id, CreateSpecializationDto dto)
    {
        if (repository.GetById(id) is null)
        {
            return false;
        }

        Validate(dto);

        var specialization = new Specialization
        {
            Name = dto.Name
        };

        return repository.Update(id, specialization);
    }

    /// <summary>
    /// Удалить специализацию.
    /// </summary>
    public bool Delete(int id) => repository.Delete(id);

    /// <summary>
    /// Проверить название специализации.
    /// </summary>
    private static void Validate(CreateSpecializationDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ArgumentException(
                "Название специализации не может быть пустым.");
        }
    }

    /// <summary>
    /// Преобразовать сущность в DTO.
    /// </summary>
    private static SpecializationDto ToDto(
        Specialization specialization) => new(
            specialization.Id,
            specialization.Name);
}

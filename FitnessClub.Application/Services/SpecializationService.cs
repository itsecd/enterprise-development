using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// CRUD-сервис специализаций
/// </summary>
public class SpecializationService(ISpecializationRepository repository)
    : CrudServiceBase<Specialization, SpecializationDto, SpecializationEditDto>(repository), ISpecializationService
{
    /// <inheritdoc />
    protected override SpecializationDto MapToDto(Specialization entity) => entity.ToDto();

    /// <inheritdoc />
    protected override Task<Specialization> MapToEntityAsync(SpecializationEditDto editDto) =>
        Task.FromResult(new Specialization { Name = editDto.Name });
}
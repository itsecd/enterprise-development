namespace VetClinic.Application.Contracts.Specializations;

/// <summary>
/// Интерфейс прикладного сервиса для управления справочником врачебных специализаций.
/// </summary>
public interface ISpecializationAppService : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>;
